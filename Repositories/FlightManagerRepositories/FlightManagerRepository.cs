using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Repositories.FlightRepositories;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.FlightManagerRepositories;

public class FlightManagerRepository : IFlightManagerRepository
{
    private readonly IDataSource _dataSource;
    private readonly IFlightRepository _flightRepository;

    public FlightManagerRepository(IDataSource dataSource, IFlightRepository flightRepository)
    {
        _dataSource = dataSource;
        _flightRepository = flightRepository;
    }

    public void AddFlightManager(FlightManager flightManager)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            records.Add(FlightManagerHandler.GetAttributesFromFlightManager(flightManager));
            _dataSource.WriteToDataSource(records);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while adding the flightManager, so the flightManager was not added.");
        }
    }

    public FlightManager? FindFlightManager(string id)
    {
        try
        {
            return (from flightManager in GetFlightManagers() 
                where flightManager.FlightManagerId.Equals(Guid.Parse(id)) 
                select flightManager).Single();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public IEnumerable<FlightManager> GetFlightManagers()
    {
        try
        {
            return _dataSource.GetRecordsFromDataSource()
                .Select(FlightManagerHandler.CreateFlightManager)
                .Select(flightManager =>
                {
                    flightManager.FlightsManaged = _flightRepository
                        .GetManagedFlightByManager(flightManager.FlightManagerId.ToString());
                    return flightManager;
                });
        }
        catch (Exception e)
        {
            Console.WriteLine("The CSV file format is inconsistent.");
        }
        return new List<FlightManager>();
    }

    public void UpdateFlightManager(FlightManager flightManager)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            var isFound = false;
            for (var i = 0; i < records.Count; i++)
            {
                if (!records[i][0].Equals(flightManager.FlightManagerId.ToString())) continue;
                records[i] = FlightManagerHandler.GetAttributesFromFlightManager(flightManager);
                isFound = true;
                break;
            }
            if(isFound) _dataSource.WriteToDataSource(records);
            else Console.WriteLine("FlightManager with the given ID doesn't exist.");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while updating the flightManager.");
        }
    }

    public void DeleteFlightManager(string id)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            _dataSource.WriteToDataSource(from record in records where !record[0].Equals(id) select record);
            _flightRepository.UnassignManagerFromFlights(id);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the flightManager, so the flightManager was not removed.");
        }
    }
}