using AirportTicketBooking.DataAccessLayer;
using AirportTicketBooking.Repositories.CabinRepositories;
using AirportTicketBooking.Repositories.FlightRepositories;
using AirportTicketBooking.Repositories.PassengerRepositories;
using AirportTicketBooking.Repositories.TicketRepositories;
using AirportTicketBooking.Utils;

namespace AirportTicketBooking;

public class Program
{
    public static void Main(string[] args)
    {
        var cabinRepo = new CabinRepository(new FileHandler(FilesPaths.CabinFilePath));
        var ticketRepo = new TicketRepository(new FileHandler(FilesPaths.TicketFilePath));
        var passengerRepo = new PassengerRepository(new FileHandler(FilesPaths.PassengerFilePath));
        var repo = new FlightRepository(new FileHandler(FilesPaths.FlightFilePath),cabinRepo,ticketRepo,passengerRepo);
        repo.DeleteFlight("38711a60-f873-440f-b40e-c0365b187b12");
    }
}