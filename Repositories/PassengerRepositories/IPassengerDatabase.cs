using AirportTicketBookingDomain;

namespace AirportTicketBooking.Repositories.PassengerRepositories;

public interface IPassengerDatabase
{
    public void AddPassenger(Passenger passenger);
    public Passenger? FindPassenger(string id);
    public IEnumerable<Passenger> GetPassengers();
    public void UpdatePassenger(Passenger passenger);
    public void DeletePassenger(string id);
}