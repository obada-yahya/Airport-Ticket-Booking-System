namespace AirportTicketBookingDomain;

#nullable disable
public class FlightManager
{
    public Guid FlightManagerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Flight> FlightsManaged { get; set; }
}