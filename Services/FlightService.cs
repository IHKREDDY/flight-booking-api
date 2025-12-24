using FlightBookingAPI.Models;

namespace FlightBookingAPI.Services;

public interface IFlightService
{
    Task<IEnumerable<Flight>> SearchFlightsAsync(FlightSearchRequest request);
    Task<Flight?> GetFlightByIdAsync(int id);
    Task<IEnumerable<Flight>> GetAllFlightsAsync();
}

public class FlightService : IFlightService
{
    private readonly List<Flight> _flights;

    public FlightService()
    {
        // Sample flight data
        _flights = new List<Flight>
        {
            new Flight
            {
                Id = 1,
                FlightNumber = "IHK-101",
                Origin = "HYD",
                Destination = "BLR",
                DepartureTime = DateTime.Today.AddDays(1).AddHours(6),
                ArrivalTime = DateTime.Today.AddDays(1).AddHours(7).AddMinutes(30),
                Price = 3500,
                AvailableSeats = 45,
                Airline = "IndiGo"
            },
            new Flight
            {
                Id = 2,
                FlightNumber = "IHK-202",
                Origin = "HYD",
                Destination = "DEL",
                DepartureTime = DateTime.Today.AddDays(1).AddHours(8),
                ArrivalTime = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(15),
                Price = 5200,
                AvailableSeats = 32,
                Airline = "Air India"
            },
            new Flight
            {
                Id = 3,
                FlightNumber = "IHK-303",
                Origin = "BLR",
                Destination = "MUM",
                DepartureTime = DateTime.Today.AddDays(2).AddHours(14),
                ArrivalTime = DateTime.Today.AddDays(2).AddHours(15).AddMinutes(45),
                Price = 4100,
                AvailableSeats = 28,
                Airline = "SpiceJet"
            },
            new Flight
            {
                Id = 4,
                FlightNumber = "IHK-404",
                Origin = "DEL",
                Destination = "HYD",
                DepartureTime = DateTime.Today.AddDays(3).AddHours(10),
                ArrivalTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(30),
                Price = 4800,
                AvailableSeats = 50,
                Airline = "Vistara"
            }
        };
    }

    public Task<IEnumerable<Flight>> GetAllFlightsAsync()
    {
        return Task.FromResult(_flights.AsEnumerable());
    }

    public Task<Flight?> GetFlightByIdAsync(int id)
    {
        return Task.FromResult(_flights.FirstOrDefault(f => f.Id == id));
    }

    public Task<IEnumerable<Flight>> SearchFlightsAsync(FlightSearchRequest request)
    {
        var results = _flights.Where(f =>
            f.Origin.Equals(request.Origin, StringComparison.OrdinalIgnoreCase) &&
            f.Destination.Equals(request.Destination, StringComparison.OrdinalIgnoreCase) &&
            f.DepartureTime.Date == request.DepartureDate.Date &&
            f.AvailableSeats >= request.Passengers);

        return Task.FromResult(results);
    }
}
