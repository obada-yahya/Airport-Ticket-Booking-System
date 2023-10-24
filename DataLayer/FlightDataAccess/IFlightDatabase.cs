using AirportTicketBookingDomain;

namespace AirportTicketBooking.DataLayer;

public interface IFlightDatabase
{
    public void AddFlight(Flight flight);
    public Flight? FindFlight(string id);
    public IEnumerable<Flight> GetFlights();
    public void UpdateFlights(Flight flight);
    public void DeleteFlight(string id);
}