using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.CabinRepositories;

public class CabinRepository : ICabinRepository
{
     private readonly IDataSource _dataSource; 
    
    public CabinRepository(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public void AddCabin(Cabin cabin)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            records.Add(CabinHandler.GetAttributesFromFlightCabin(cabin));
            _dataSource.WriteToDataSource(records);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while adding the cabin, so the cabin was not added.");
        }
    }

    public Cabin? FindCabin(string id)
    {
        try
        {
            return (from cabin in GetCabins() 
                where cabin.CabinId.Equals(Guid.Parse(id)) 
                select cabin).Single();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public IEnumerable<Cabin> GetCabins()
    {
        try
        {
            return (from record in _dataSource.GetRecordsFromDataSource() 
                select CabinHandler.CreateFlightCabin(record)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("The CSV file format is inconsistent.");
        }
        return new List<Cabin>();
    }

    public void UpdateCabin(Cabin cabin)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            var isFound = false;
            for (var i = 0; i < records.Count; i++)
            {
                if (!records[i][0].Equals(cabin.CabinId.ToString())) continue;
                records[i] = CabinHandler.GetAttributesFromFlightCabin(cabin);
                isFound = true;
                break;
            }
            if(isFound) _dataSource.WriteToDataSource(records);
            else Console.WriteLine("Cabin with the given ID doesn't exist.");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while updating the cabin");
        }
    }

    public void DeleteCabin(string id)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            _dataSource.WriteToDataSource(from record in records where !record[0].Equals(id) select record);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the cabin, so the cabin was not removed.");
        }
    }

    public Dictionary<CabinClass, float> GetFlightCabins(string flightId)
    {
        try
        {
            var cabins = (from cabin in GetCabins() 
                where cabin.FlightId.Equals(Guid.Parse(flightId)) 
                select cabin);
            return CabinHandler.GetCabinsPrices(cabins);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new Dictionary<CabinClass, float>();
    }

    public void DeleteFlightCabins(string flightId)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            _dataSource.WriteToDataSource(from record in records where !record[3].Equals(flightId) select record);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the Flight cabins, so the cabins was not removed.");
        }
    }
}