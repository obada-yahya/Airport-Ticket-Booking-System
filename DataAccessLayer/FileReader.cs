namespace AirportTicketBooking.DataAccessLayer;

public abstract class FileReader
{
    private readonly string _filePath;

    protected FileReader()
    {
        _filePath = GetFilePath();
    }

    protected abstract string GetFilePath();
    
    private string[] GetCsvFileHeaders()
    {
        using var reader = new StreamReader(_filePath);
        var line = reader.ReadLine();
        if (line is null) throw new Exception("An issue occurred while reading the CSV file");
        return line.Split(",");
    }
    
    protected IEnumerable<string[]> ReadFileRecords()
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
    
    protected void WriteToFile(IEnumerable<string[]> records)
    {
        var headers = GetCsvFileHeaders();
        using var streamWriter = new StreamWriter(_filePath);
        streamWriter.WriteLine(string.Join(',',headers));
        foreach (var record in records) streamWriter.WriteLine(string.Join(',',record));
    }
}