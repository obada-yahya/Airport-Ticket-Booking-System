using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.FlightManagerServices;

public interface IFlightManagerService
{
    public void AddFlightManager(FlightManager flightManager);
    public FlightManager? FindFlightManager(string id);
    public IEnumerable<FlightManager> GetFlightManagers();
    public void UpdateFlightManager(FlightManager flightManager);
    public void DeleteFlightManager(string id);
}