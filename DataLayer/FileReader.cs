namespace AirportTicketBooking.DataLayer;

public abstract class FileReader
{
    protected string _filePath;
    
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
}