using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.TicketRepositories;

public interface ITicketRepository
{
    public void AddTicket(Ticket ticket);
    public Ticket? FindTicket(string id);
    public IEnumerable<Ticket> GetTickets();
    public void UpdateTicket(Ticket ticket);
    public void DeleteTicket(string id);
}