using AirportTicketBooking.Repositories.FlightRepositories;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.FlightServices;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;

    public FlightService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public void AddFlight(Flight flight)
    {
        _flightRepository.AddFlight(flight);
    }

    public Flight? FindFlight(string id)
    {
        return _flightRepository.FindFlight(id);
    }

    public IEnumerable<Flight> GetFlights()
    {
        return _flightRepository.GetFlights();
    }

    public void UpdateFlight(Flight flight)
    {
        _flightRepository.UpdateFlight(flight);
    }

    public void DeleteFlight(string id)
    {
        _flightRepository.DeleteFlight(id);
    }

    public IEnumerable<Flight> GetManagedFlightByManager(string flightManagerId)
    {
        return _flightRepository.GetManagedFlightByManager(flightManagerId);
    }

    public void UnassignManagerFromFlights(string flightManagerId)
    {
        _flightRepository.UnassignManagerFromFlights(flightManagerId);
    }
}