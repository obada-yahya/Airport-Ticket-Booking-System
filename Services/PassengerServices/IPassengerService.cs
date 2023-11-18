using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.PassengerServices;

public interface IPassengerService
{
    public void AddPassenger(Passenger passenger);
    public Passenger? FindPassenger(string id);
    public IEnumerable<Passenger> GetPassengers();
    public void UpdatePassenger(Passenger passenger);
    public void DeletePassenger(string id);
    public void ApplyPassengerRefunds(IEnumerable<(Guid,float)> passengerRefunds);
}