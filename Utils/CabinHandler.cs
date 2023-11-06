using AirportTicketBookingDomain;

namespace AirportTicketBooking.Utils;

public static class CabinHandler
{
    public static Cabin CreateFlightCabin(IReadOnlyList<string> attributes)
    {
        Enum.TryParse(attributes[1],out CabinClass cabinClass);
        return new Cabin()
        {
            CabinId = Guid.Parse(attributes[0]),
            CabinName = cabinClass,
            Price = float.Parse(attributes[2]),
            FlightId = Guid.Parse(attributes[3])
        };
    }

    public static string[] GetAttributesFromFlightCabin(Cabin cabin)
    {
        return new[]
        {
            cabin.CabinId.ToString(),
            cabin.CabinName.ToString(),
            cabin.Price.ToString(),
            cabin.FlightId.ToString()
        };
    }
}