using System.Globalization;
using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.PassengerRepositories;

public class PassengerRepository : IPassengerRepository
{
    private readonly IDataSource _dataSource; 
    private const int PassengerIdColumn = 0;
    
    public PassengerRepository(IDataSource dataSource)
    { 
        _dataSource = dataSource;
    }

    public void AddPassenger(Passenger passenger)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            records.Add(PassengerHandler.GetAttributesFromPassenger(passenger));
            _dataSource.WriteToDataSource(records);
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
            return (from record in _dataSource.GetRecordsFromDataSource() 
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
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            var isFound = false;
            for (var i = 0; i < records.Count; i++)
            {
                if (!records[i][PassengerIdColumn].Equals(passenger.PassengerId.ToString())) continue;
                records[i] = PassengerHandler.GetAttributesFromPassenger(passenger);
                isFound = true;
                break;
            }
            if(isFound) _dataSource.WriteToDataSource(records);
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
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            _dataSource.WriteToDataSource(from record in records where !record[PassengerIdColumn].Equals(id) select record);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the passenger, so the passenger was not removed.");
        }
    }

    public void ApplyPassengerRefunds(IEnumerable<(Guid, float)> passengerRefunds)
    {
        const int accountBalanceColumn = 3;
        var passengerRefundsDictionary = new Dictionary<Guid, float>();
        try
        {
            foreach (var passengerRefund in passengerRefunds)
            {
                if (passengerRefundsDictionary.ContainsKey(passengerRefund.Item1))
                    passengerRefundsDictionary[passengerRefund.Item1] += passengerRefund.Item2;
                else passengerRefundsDictionary.Add(passengerRefund.Item1, passengerRefund.Item2);
            }
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            foreach (var record in records)
            {
                var passengerId = Guid.Parse(record[PassengerIdColumn]);
                if (passengerRefundsDictionary.TryGetValue(passengerId, out var refund))
                {
                    record[accountBalanceColumn] = (float.Parse(record[accountBalanceColumn]) + refund)
                        .ToString(CultureInfo.CurrentCulture);
                }
            }
            _dataSource.WriteToDataSource(records);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}