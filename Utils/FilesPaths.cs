namespace AirportTicketBooking.Utils;

public static class FilesPaths
{
    public static readonly string FlightFilePath = GetFlightFilePath();
    public static readonly string PassengerFilePath = GetPassengerFilePath();
    public static readonly string TicketFilePath = GetTicketFilePath();

    private static string GetPassengerFilePath()
    {
        var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
        var filePath = $@"{parentPath}\Seeds\Passengers.csv";
        if (parentPath == null || !File.Exists(filePath)) throw new FileNotFoundException("Invalid File Path");
        return filePath;
    }
    
    private static string GetFlightFilePath()
    {
        var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
        var filePath = $@"{parentPath}\Seeds\Flights.csv";
        if (parentPath == null || !File.Exists(filePath)) throw new FileNotFoundException("Invalid File Path");
        return filePath;
    }
    
    private static string GetTicketFilePath()
    {
        var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
        var filePath = $@"{parentPath}\Seeds\Tickets.csv";
        if (parentPath == null || !File.Exists(filePath)) throw new FileNotFoundException("Invalid File Path");
        return filePath;
    }
}