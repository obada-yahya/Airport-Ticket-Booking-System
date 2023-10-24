using AirportTicketBookingDomain;

namespace AirportTicketBooking.Utils;

public static class FlightHandler
{
    public static Flight CreateFlight(IReadOnlyList<string> attributes)
    {
        return new Flight()
        {
            FlightId= Guid.Parse(attributes[0]),
            FlightManagerId = Guid.Parse(attributes[1]),
            Name = attributes[2],
            DepartureCountry = attributes[3],
            DepartureAirport = attributes[4],
            DepartureDate = DateTime.Parse(attributes[5]),
            DestinationCountry = attributes[6],
            ArrivalAirport = attributes[7],
            ArrivalDate = DateTime.Parse(attributes[8]),
            Capacity = int.Parse(attributes[9])
        };
    }

    public static string[] GetAttributesFromFlight(Flight flight)
    {
        return new[]
        {
            flight.FlightId.ToString(),
            flight.FlightManagerId.ToString(),
            flight.Name,
            flight.DepartureCountry,
            flight.DepartureAirport,
            flight.DepartureDate.ToString("yyyy-MM-dd HH:mm"),
            flight.DestinationCountry,
            flight.ArrivalAirport,
            flight.ArrivalDate.ToString("yyyy-MM-dd HH:mm"),
            flight.Capacity.ToString() 
        };
    }
}