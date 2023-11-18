using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.FlightServices;

public interface IFlightService
{
    public void AddFlight(Flight flight);
    public Flight? FindFlight(string id);
    public IEnumerable<Flight> GetFlights();
    public void UpdateFlight(Flight flight);
    public void DeleteFlight(string id);
    public IEnumerable<Flight> GetManagedFlightByManager(string flightManagerId);
    public void UnassignManagerFromFlights(string flightManagerId);
}