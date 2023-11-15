using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Utils;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.TicketRepositories;

public class TicketRepository : ITicketRepository
{
    private readonly IDataSource _dataSource; 
    private const int TicketIdColumn = 0;
    
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
                if (!records[i][TicketIdColumn].Equals(ticket.PassengerId.ToString())) continue;
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

    public (Guid, float)? ReleaseTicketRefund(string id)
    {
        const int passengerIdColumn = 2;
        const int ticketPriceColumn = 3;
        const int ticketStatusColumn = 4;
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            var ticket = records.SingleOrDefault(record => record[TicketIdColumn].Equals(id));
            if (ticket is null)
            {
                Console.WriteLine("Ticket with the given ID doesn't exist.");
                return null;
            }

            Enum.TryParse<TicketStatus>(ticket[ticketStatusColumn], true, out var ticketStatus);

            if (!ticketStatus.Equals(TicketStatus.Booked))
                throw new InvalidOperationException("Ticket Status is Not Booked.");

            ticket[ticketStatusColumn] = TicketStatus.Available.ToString();
            var result = (Guid.Parse(ticket[passengerIdColumn]), float.Parse(ticket[ticketPriceColumn]));
            ticket[passengerIdColumn] = string.Empty;
            _dataSource.WriteToDataSource(records);
            return result;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while Releasing the ticket, so the ticket was not removed.");
        }
        return null;
    }

    public IEnumerable<(Guid, float)> CancelAllTicketsForFlight(string flightId)
    {
        var result = new List<(Guid,float)>();
        const int flightIdColumn = 1;
        const int passengerIdColumn = 2;
        const int ticketPriceColumn = 3;
        const int ticketStatusColumn = 4;
        try
        {
            var records = _dataSource.GetRecordsFromDataSource().ToList();
            var tickets = records.Where(record => record[flightIdColumn].Equals(flightId));
            foreach (var ticket in tickets)
            {
                Enum.TryParse<TicketStatus>(ticket[ticketStatusColumn], true, out var ticketStatus);
                if (ticketStatus.Equals(TicketStatus.Cancelled)) continue;
                ticket[ticketStatusColumn] = TicketStatus.Cancelled.ToString();
                result.Add((Guid.Parse(ticket[passengerIdColumn]), float.Parse(ticket[ticketPriceColumn])));
                _dataSource.WriteToDataSource(records);
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while Canceling the flight tickets, so the tickets was not removed.");
        }
        return result;
    }
}