using AirportTicketBooking.Repositories.CabinRepositories;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.CabinServices;

public class CabinService : ICabinService
{
    private readonly ICabinRepository _cabinRepository;
    
    public CabinService(ICabinRepository cabinRepository)
    {
        _cabinRepository = cabinRepository;
    }
    public void AddCabin(Cabin cabin)
    {
        _cabinRepository.AddCabin(cabin);
    }

    public Cabin? FindCabin(string id)
    {
        return _cabinRepository.FindCabin(id);
    }

    public IEnumerable<Cabin> GetCabins()
    {
        return _cabinRepository.GetCabins();
    }

    public void UpdateCabin(Cabin cabin)
    {
        _cabinRepository.UpdateCabin(cabin);
    }

    public void DeleteCabin(string id)
    {
        _cabinRepository.DeleteCabin(id);
    }

    public Dictionary<CabinClass, float> GetFlightCabins(string flightId)
    {
        return _cabinRepository.GetFlightCabins(flightId);
    }

    public void DeleteFlightCabins(string flightId)
    {
        _cabinRepository.DeleteFlightCabins(flightId);
    }
}