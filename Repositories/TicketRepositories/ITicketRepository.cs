using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.TicketRepositories;

public interface ITicketRepository
{
    public void AddTicket(Ticket ticket);
    public Ticket? FindTicket(string id);
    public IEnumerable<Ticket> GetTickets();
    public void UpdateTicket(Ticket ticket);
    public (Guid, float)? ReleaseTicketRefund(string id);
    public IEnumerable<(Guid, float)> CancelAllTicketsForFlight(string flightId);
}