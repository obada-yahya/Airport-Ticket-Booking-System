namespace AirportTicketBookingDomain;

public class Cabin
{
    public Guid CabinId { get; set; }
    public CabinClass CabinName { get; set; }
    public float Price { get; set; }
    public Guid FlightId { get; set; }
}