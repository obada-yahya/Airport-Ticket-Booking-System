namespace AirportTicketBookingDomain;

#nullable disable
public class Passenger
{
    public Guid PassengerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public float AccountBalance { get; set; }
    public List<Ticket> Tickets { get; set; }
}