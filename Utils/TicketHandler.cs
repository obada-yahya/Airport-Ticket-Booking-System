using AirportTicketBookingDomain;

namespace AirportTicketBooking.Utils;

public static class TicketHandler
{
    public static Ticket CreateTicket(IReadOnlyList<string> attributes)
    {
        return new Ticket()
        {
            TicketId = Guid.Parse(attributes[0]),
            FlightId = Guid.Parse(attributes[1]),
            PassengerId = Guid.Parse(attributes[2]),
            Price = float.Parse(attributes[3]),
            Status = (TicketStatus)Enum.Parse(typeof(TicketStatus), attributes[4]),
        };
    }

    public static string[] GetAttributesFromTicket(Ticket ticket)
    {
        return new[]
        {
            ticket.TicketId.ToString(),
            ticket.FlightId.ToString(),
            ticket.PassengerId.ToString(),
            ticket.Price.ToString(),
            ticket.Status.ToString()
        };
    }
}