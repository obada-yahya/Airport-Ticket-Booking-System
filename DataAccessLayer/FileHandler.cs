namespace AirportTicketBooking.DataAccessLayer;

public class FileHandler : IDataSource
{
    private readonly string _filePath;

    private FileHandler(string filePath)
    {
        _filePath = filePath;
    }
    
    private string[] GetCsvFileHeaders()
    {
        using var reader = new StreamReader(_filePath);
        var line = reader.ReadLine();
        if (line is null) throw new Exception("An issue occurred while reading the CSV file");
        return line.Split(",");
    }

    public IEnumerable<string[]> GetRecordsFromDataSource()
    {
        using var reader = new StreamReader(_filePath);
        var isHeader = true;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line is null) throw new Exception("An issue occurred while reading the CSV file");
            if (isHeader)
            {
                isHeader = false;
                continue;
            }
            yield return (from value in line.Split(",") select value.Trim()).ToArray();
        }
    }

    public void WriteToDataSource(IEnumerable<string[]> records)
    {
        var headers = GetCsvFileHeaders();
        using var streamWriter = new StreamWriter(_filePath);
        streamWriter.WriteLine(string.Join(',',headers));
        foreach (var record in records) streamWriter.WriteLine(string.Join(',',record));
    }
}