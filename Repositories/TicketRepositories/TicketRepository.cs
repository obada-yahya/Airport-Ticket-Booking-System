using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.TicketRepositories;

public class TicketRepository : ITicketRepository
{
    private readonly IDataSource _dataSource; 
    
    public TicketRepository(IDataSource dataSource)
    { 
        _dataSource = dataSource;
    }
    
    public void AddTicket(Ticket ticket)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            records.Add(TicketHandler.GetAttributesFromTicket(ticket));
            _dataSource.WriteToDataSource(records);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while adding the ticket, so the ticket was not added.");
        }
    }

    public Ticket? FindTicket(string id)
    {
        try
        {
            return (from ticket in GetTickets() 
                where ticket.TicketId.Equals(Guid.Parse(id)) 
                select ticket).Single();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public IEnumerable<Ticket> GetTickets()
    {
        try
        {
            return (from record in _dataSource.GetRecordsFromDataSource() 
                select TicketHandler.CreateTicket(record)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("The CSV file format is inconsistent.");
        }
        return new List<Ticket>();
    }

    public void UpdateTicket(Ticket ticket)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            var isFound = false;
            for (var i = 0; i < records.Count; i++)
            {
                if (!records[i][0].Equals(ticket.PassengerId.ToString())) continue;
                records[i] = TicketHandler.GetAttributesFromTicket(ticket);
                isFound = true;
                break;
            }
            if(isFound) _dataSource.WriteToDataSource(records);
            else Console.WriteLine("Ticket with the given ID doesn't exist.");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while updating the ticket");
        }
    }

    public void DeleteTicket(string id)
    {
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            _dataSource.WriteToDataSource(from record in records where !record[0].Equals(id) select record);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the ticket, so the ticket was not removed.");
        }
    }
}