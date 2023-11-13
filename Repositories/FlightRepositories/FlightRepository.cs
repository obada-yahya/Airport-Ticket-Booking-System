using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Repositories.CabinRepositories;
using AirportTicketBooking.Repositories.PassengerRepositories;
using AirportTicketBooking.Repositories.TicketRepositories;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.FlightRepositories;

public class FlightRepository : IFlightRepository
{
    private readonly IDataSource _dataSource;
    private readonly ICabinRepository _cabinRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IPassengerRepository _passengerRepository;
    private const int FlightIdColumn = 0;
    
    public FlightRepository(IDataSource dataSource, ICabinRepository cabinRepository, ITicketRepository ticketRepository, IPassengerRepository passengerRepository)
    {
        _dataSource = dataSource;
        _cabinRepository = cabinRepository;
        _ticketRepository = ticketRepository;
        _passengerRepository = passengerRepository;
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
                if (!records[i][FlightIdColumn].Equals(flight.FlightId.ToString())) continue;
                records[i] = FlightHandler.GetAttributesFromFlight(flight);
                isFound = true;
                break;
            }
            if(isFound) _dataSource.WriteToDataSource(records);
            else Console.WriteLine("Flight with the given ID doesn't exist.");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while updating the flight.");
        }
    }
    
    public void DeleteFlight(string id)
    {
        const int isDeleteColumn = 10;
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            var flightRecord = records.FirstOrDefault(record => record[FlightIdColumn].Equals(id));
            if (flightRecord[isDeleteColumn].ToLower().Equals("true")) 
                throw new InvalidOperationException("Flight Already Deleted");
            flightRecord[isDeleteColumn] = "true";
            _dataSource.WriteToDataSource(records);
            _cabinRepository.DeleteFlightCabins(id);
            _passengerRepository.ApplyPassengerRefunds(_ticketRepository.CancelAllTicketsForFlight(id));
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the flight, so the flight was not removed.");
        }
    }

    public IEnumerable<Flight> GetManagedFlightByManager(string flightManagerId)
    {
        return (from flight in GetFlights() where flight.FlightManagerId.ToString().Equals(flightManagerId) select flight);
    }

    public void UnassignManagerFromFlights(string managerId)
    {
        try
        {
            const int flightManagerIdColumn = 1;
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            foreach (var record in records.Where(record => record[flightManagerIdColumn].Equals(managerId)))
            {
                record[flightManagerIdColumn] = string.Empty;
            }
            _dataSource.WriteToDataSource(records);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while Unassign the FlightManager from the flights, so the flights was not removed.");
        }
    }
}