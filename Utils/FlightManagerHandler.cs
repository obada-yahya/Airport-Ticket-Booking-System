using AirportTicketBookingDomain;

namespace AirportTicketBooking.Utils;

public class FlightManagerHandler
{
    public static FlightManager CreateFlightManager(IReadOnlyList<string> attributes)
    {
        return new FlightManager()
        {
            FlightManagerId = Guid.Parse(attributes[0]),
            FirstName = attributes[1],
            LastName = attributes[2]
        };
    }

    public static string[] GetAttributesFromFlightManager(FlightManager flightManager)
    {
        return new[]
        {
            flightManager.FlightManagerId.ToString(),
            flightManager.FirstName,
            flightManager.LastName
        };
    }
}