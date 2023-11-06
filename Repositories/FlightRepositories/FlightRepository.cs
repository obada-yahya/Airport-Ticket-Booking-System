using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Repositories.CabinRepositories;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.FlightRepositories;

public class FlightRepository : IFlightRepository
{
    private readonly IDataSource _dataSource;
    private readonly ICabinRepository _cabinRepository;
    
    public FlightRepository(IDataSource dataSource, ICabinRepository cabinRepository)
    {
        _dataSource = dataSource;
        _cabinRepository = cabinRepository;
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
            return (from flight in GetFlights() 
                where flight.FlightId.Equals(Guid.Parse(id)) 
                select flight).Single();
            
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
            return _dataSource.GetRecordsFromDataSource()
                .Select(FlightHandler.CreateFlight)
                .Select(flight =>
                {
                    flight.CabinPrices = _cabinRepository.GetFlightCabins(flight.FlightId.ToString());
                    return flight;
                });
        }
        catch (Exception e)
        {
            Console.WriteLine("The CSV file format is inconsistent.");
        }
        return new List<Flight>();
    }
    
    public void UpdateFlight(Flight flight)
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
            _cabinRepository.DeleteFlightCabins(id);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the flight, so the flight was not removed.");
        }
    }
}