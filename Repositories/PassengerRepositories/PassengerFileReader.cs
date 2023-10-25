using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.PassengerRepositories;

public sealed class PassengerFileReader : FileReader, IPassengerDatabase
{
    private static PassengerFileReader? _instance;

    public static PassengerFileReader Instance
    {
        get { return _instance ??= new PassengerFileReader(); }
    }

    private PassengerFileReader() {}

    public void AddPassenger(Passenger passenger)
    {
        try
        {
            var records = ReadFileRecords().ToList();
            records.Add(PassengerHandler.GetAttributesFromPassenger(passenger));
            WriteToFile(records);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while adding the passenger, so the passenger was not added.");
        }
    }
    
    public Passenger? FindPassenger(string id)
    {
        try
        {
            return (from passenger in GetPassengers() 
                where passenger.PassengerId.Equals(Guid.Parse(id)) 
                select passenger).Single();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }
    
    public IEnumerable<Passenger> GetPassengers()
    {
        try
        {
            return (from record in ReadFileRecords() 
                select PassengerHandler.CreatePassenger(record)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("The CSV file format is inconsistent.");
        }
        return new List<Passenger>();
    }

    public void UpdatePassenger(Passenger passenger)
    {
        try
        {
            var records = ReadFileRecords().ToList();
            var isFound = false;
            for (var i = 0; i < records.Count; i++)
            {
                if (!records[i][0].Equals(passenger.PassengerId.ToString())) continue;
                records[i] = PassengerHandler.GetAttributesFromPassenger(passenger);
                isFound = true;
                break;
            }
            if(isFound) WriteToFile(records);
            else Console.WriteLine("Passenger with the given ID doesn't exist.");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while updating the passenger");
        }
    }

    public void DeletePassenger(string id)
    {
        try
        {
            var records = ReadFileRecords().ToList();
            WriteToFile(from record in records where !record[0].Equals(id) select record);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the passenger, so the passenger was not removed.");
        }
    }

    protected override string GetFilePath()
    {
        var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
        var filePath = $"{parentPath}/CsvData/Passengers.csv";
        if (parentPath == null || !File.Exists(filePath)) throw new FileNotFoundException("Invalid File Path");
        return filePath;
    }
}