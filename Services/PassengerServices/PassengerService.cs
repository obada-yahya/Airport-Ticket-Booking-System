using AirportTicketBooking.Repositories.PassengerRepositories;
using AirportTicketBookingDomain;

namespace AirportTicketBooking.Services.PassengerServices;

public class PassengerService : IPassengerService
{
    private readonly IPassengerRepository _passengerRepository;

    public PassengerService(IPassengerRepository passengerRepository)
    {
        _passengerRepository = passengerRepository;
    }

    public void AddPassenger(Passenger passenger)
    {
        _passengerRepository.AddPassenger(passenger);
    }

    public Passenger? FindPassenger(string id)
    {
        return _passengerRepository.FindPassenger(id);
    }

    public IEnumerable<Passenger> GetPassengers()
    {
        return _passengerRepository.GetPassengers();
    }

    public void UpdatePassenger(Passenger passenger)
    {
        _passengerRepository.UpdatePassenger(passenger);
    }

    public void DeletePassenger(string id)
    {
        _passengerRepository.DeletePassenger(id);
    }

    public void ApplyPassengerRefunds(IEnumerable<(Guid, float)> passengerRefunds)
    {
        _passengerRepository.ApplyPassengerRefunds(passengerRefunds);
    }
}