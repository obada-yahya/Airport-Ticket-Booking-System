namespace AirportTicketBookingDomain;

public enum TicketStatus
{
    Available,
    Cancelled,
    Booked,
}

public enum CabinClass
{
    Economy,
    PremiumEconomy,
    Business,
    FirstClass
}

public class Ticket
{
    public Guid TicketId { get; set; }
    public Guid FlightId { get; set; }
    public Guid PassengerId { get; set; }
    public float Price { get; set; } 
    public TicketStatus Status { get; set; }
    public CabinClass CabinClass { get; set; }
}