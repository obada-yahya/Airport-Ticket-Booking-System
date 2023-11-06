using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.FlightManagerRepositories;

public interface IFlightManagerRepository
{
    public void AddFlightManager(FlightManager flightManager);
    public FlightManager? FindFlightManager(string id);
    public IEnumerable<FlightManager> GetFlightManagers();
    public void UpdateFlightManager(FlightManager flightManager);
    public void DeleteFlightManager(string id);
}