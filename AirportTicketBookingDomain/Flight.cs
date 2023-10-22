namespace AirportTicketBookingDomain;

#nullable disable
public class Flight
{
    public Guid FlightId { get; set; }
    public Guid FlightManagerId { get; set; }
    public string Name { get; set; }
    public string DepartureCountry {get; set; }
    public string DepartureAirport { get; set; }
    public DateTime DepartureDate { get; set; }
    public string DestinationCountry { get; set; }
    public string ArrivalAirport {get; set; }
    public DateTime ArrivalDate { get; set; }
    public Dictionary<CabinClass, float> CabinPrices { get; set; }
    public int Capacity { get; set; }
    public List<Ticket> FlyingTickets { get; set; }
}