using AirportTicketBooking.Repositories.FlightManagerRepositories;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.FlightManagerServices;

public class  FlightManagerService : IFlightManagerService
{
    private readonly IFlightManagerRepository _flightManagerRepository;

    public FlightManagerService(IFlightManagerRepository flightManagerRepository)
    {
        _flightManagerRepository = flightManagerRepository;
    }

    public void AddFlightManager(FlightManager flightManager)
    {
        _flightManagerRepository.AddFlightManager(flightManager);
    }

    public FlightManager? FindFlightManager(string id)
    {
        return _flightManagerRepository.FindFlightManager(id);
    }

    public IEnumerable<FlightManager> GetFlightManagers()
    {
        return _flightManagerRepository.GetFlightManagers();
    }

    public void UpdateFlightManager(FlightManager flightManager)
    {
        _flightManagerRepository.UpdateFlightManager(flightManager);
    }

    public void DeleteFlightManager(string id)
    {
        _flightManagerRepository.DeleteFlightManager(id);
    }
}