using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.FlightRepositories;

public interface IFlightRepository
{
    public void AddFlight(Flight flight);
    public Flight? FindFlight(string id);
    public IEnumerable<Flight> GetFlights();
    public void UpdateFlights(Flight flight);
    public void DeleteFlight(string id);
}