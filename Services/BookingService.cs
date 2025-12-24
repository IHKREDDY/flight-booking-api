using FlightBookingAPI.Models;

namespace FlightBookingAPI.Services;

public interface IBookingService
{
    Task<Booking> CreateBookingAsync(BookingRequest request);
    Task<Booking?> GetBookingAsync(string reference);
    Task<Booking?> CancelBookingAsync(string reference);
    Task<IEnumerable<Booking>> GetAllBookingsAsync();
}

public class BookingService : IBookingService
{
    private readonly List<Booking> _bookings = new();
    private readonly IFlightService _flightService;
    private int _nextId = 1;

    public BookingService(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<Booking> CreateBookingAsync(BookingRequest request)
    {
        var flight = await _flightService.GetFlightByIdAsync(request.FlightId);
        
        if (flight == null)
            throw new ArgumentException("Flight not found");
        
        if (flight.AvailableSeats < request.NumberOfSeats)
            throw new InvalidOperationException("Not enough seats available");

        var booking = new Booking
        {
            Id = _nextId++,
            BookingReference = GenerateReference(),
            FlightId = request.FlightId,
            Flight = flight,
            PassengerName = request.PassengerName,
            PassengerEmail = request.PassengerEmail,
            PassengerPhone = request.PassengerPhone,
            NumberOfSeats = request.NumberOfSeats,
            TotalPrice = flight.Price * request.NumberOfSeats,
            Status = BookingStatus.Confirmed,
            BookingDate = DateTime.UtcNow
        };

        _bookings.Add(booking);
        flight.AvailableSeats -= request.NumberOfSeats;

        return booking;
    }

    public Task<Booking?> GetBookingAsync(string reference)
    {
        return Task.FromResult(_bookings.FirstOrDefault(b => 
            b.BookingReference.Equals(reference, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<Booking?> CancelBookingAsync(string reference)
    {
        var booking = _bookings.FirstOrDefault(b => 
            b.BookingReference.Equals(reference, StringComparison.OrdinalIgnoreCase));
        
        if (booking != null && booking.Status == BookingStatus.Confirmed)
        {
            booking.Status = BookingStatus.Cancelled;
            if (booking.Flight != null)
            {
                booking.Flight.AvailableSeats += booking.NumberOfSeats;
            }
        }

        return Task.FromResult(booking);
    }

    public Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        return Task.FromResult(_bookings.AsEnumerable());
    }

    private string GenerateReference()
    {
        return $"IHK{DateTime.Now:yyMMdd}{_nextId:D4}";
    }
}
