namespace AirportTicketBooking.Utils;

public static class FilesPaths
{
    public static readonly string FlightFilePath = GetFilePath("Flights");
    public static readonly string PassengerFilePath = GetFilePath("Passengers");
    public static readonly string TicketFilePath = GetFilePath("Tickets");
    public static readonly string CabinFilePath = GetFilePath("Cabins");
    public static readonly string FlightManagerFilePath = GetFilePath("FlightManagers");
    
    private static string GetFilePath(string fileName)
    {
        var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
        var filePath = $@"{parentPath}\Seeds\{fileName}.csv";
        if (parentPath == null || !File.Exists(filePath)) 
            throw new FileNotFoundException($"Invalid File Path for {fileName} seed");
        return filePath;
    }
}