namespace AirportTicketBookingDomain;

#nullable disable
public class FlightManager
{
    public Guid FlightManagerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<Flight> FlightsManaged { get; set; }
}