using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.CabinRepositories;

public interface ICabinRepository
{
    public void AddCabin(Cabin cabin);
    public Cabin? FindCabin(string id);
    public IEnumerable<Cabin> GetCabins();
    public void UpdateCabin(Cabin cabin);
    public void DeleteCabin(string id);

    public Dictionary<CabinClass, float> GetFlightCabins(string flightId);
    public void DeleteFlightCabins(string flightId);
}