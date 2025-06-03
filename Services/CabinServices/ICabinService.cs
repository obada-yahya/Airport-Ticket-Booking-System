using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.CabinServices;

public interface ICabinService
{
    public void AddCabin(Cabin cabin);
    public Cabin? FindCabin(string id);
    public IEnumerable<Cabin> GetCabins();
    public void UpdateCabin(Cabin cabin);
    public void DeleteCabin(string id);
    public Dictionary<CabinClass, float> GetFlightCabins(string flightId);
    public void DeleteFlightCabins(string flightId);
}