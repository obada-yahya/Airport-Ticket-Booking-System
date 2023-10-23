using AirportTicketBookingDomain;

namespace AirportTicketBooking.Utils;

public static class PassengerHandler
{
    public static Passenger CreatePassenger(IReadOnlyList<string> attributes)
    {
        return new Passenger()
        {
            PassengerId = Guid.Parse(attributes[0]),
            FirstName = attributes[1],
            LastName = attributes[2],
            AccountBalance = float.Parse(attributes[3]),
            Tickets = new List<Ticket>()
        };
    }
}