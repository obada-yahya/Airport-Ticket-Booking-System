using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.FlightRepositories;

public class FlightRepository : IFlightRepository
{
    private readonly IDataSource _dataSource; 
    
    public FlightRepository(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public void AddFlight(Flight flight)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            records.Add(FlightHandler.GetAttributesFromFlight(flight));
            _dataSource.WriteToDataSource(records);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while adding the flight, so the flight was not added.");
        }
    }
    
    public Flight? FindFlight(string id)
    {
        try
        {
            return (from passenger in GetFlights() 
                where passenger.FlightId.Equals(Guid.Parse(id)) 
                select passenger).Single();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }
    
    public IEnumerable<Flight> GetFlights()
    {
        try
        {
            return (from record in _dataSource.GetRecordsFromDataSource() 
                select FlightHandler.CreateFlight(record)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("The CSV file format is inconsistent.");
        }
        return new List<Flight>();
    }
    
    public void UpdateFlights(Flight flight)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            var isFound = false;
            for (var i = 0; i < records.Count; i++)
            {
                if (!records[i][0].Equals(flight.FlightId.ToString())) continue;
                records[i] = FlightHandler.GetAttributesFromFlight(flight);
                isFound = true;
                break;
            }
            if(isFound) _dataSource.WriteToDataSource(records);
            else Console.WriteLine("Flight with the given ID doesn't exist.");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while updating the flight");
        }
    }
    
    public void DeleteFlight(string id)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            _dataSource.WriteToDataSource(from record in records where !record[0].Equals(id) select record);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the flight, so the flight was not removed.");
        }
    }
    
    protected string GetFilePath()
    {
        var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
        var filePath = $"{parentPath}/CsvData/Flights.csv";
        if (parentPath == null || !File.Exists(filePath)) throw new FileNotFoundException("Invalid File Path");
        return filePath;
    }
}