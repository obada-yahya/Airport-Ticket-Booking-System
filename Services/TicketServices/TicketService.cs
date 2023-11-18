using AirportTicketBooking.Repositories.TicketRepositories;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.TicketServices;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public void AddTicket(Ticket ticket)
    {
        _ticketRepository.AddTicket(ticket);
    }

    public Ticket? FindTicket(string id)
    {
        return _ticketRepository.FindTicket(id);
    }

    public IEnumerable<Ticket> GetTickets()
    {
        return _ticketRepository.GetTickets();
    }

    public void UpdateTicket(Ticket ticket)
    {
        _ticketRepository.UpdateTicket(ticket);
    }

    public (Guid, float)? ReleaseTicketRefund(string id)
    {
        return _ticketRepository.ReleaseTicketRefund(id);
    }

    public IEnumerable<(Guid, float)> CancelAllTicketsForFlight(string flightId)
    {
        return _ticketRepository.CancelAllTicketsForFlight(flightId);
    }
}