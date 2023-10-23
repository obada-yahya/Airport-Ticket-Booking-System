using AirportTicketBookingDomain;
using AirportTicketBooking.Utils;

namespace AirportTicketBooking.DataLayer;

public sealed class PassengerFileReader : FileReader
{
    private static PassengerFileReader? _instance;

    public static PassengerFileReader Instance
    {
        get { return _instance ??= new PassengerFileReader(); }
    }

    private PassengerFileReader()
    {
        _filePath = GetFilePath();
    }
    
    public List<Passenger> GetPassengers()
    {
        try
        {
            return (from record in ReadFileRecords() select PassengerHandler.CreatePassenger(record)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("The CSV file format is inconsistent.");
        }
        return new List<Passenger>();
    }
    
    private static string GetFilePath()
    {
        var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
        var filePath = $"{parentPath}/CsvData/Passengers.csv";
        if (parentPath == null || !File.Exists(filePath)) throw new FileNotFoundException("Invalid File Path");
        return filePath;
    }
}