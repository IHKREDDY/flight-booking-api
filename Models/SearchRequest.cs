namespace FlightBookingAPI.Models;

public class FlightSearchRequest
{
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public int Passengers { get; set; } = 1;
}

public class BookingRequest
{
    public int FlightId { get; set; }
    public string PassengerName { get; set; } = string.Empty;
    public string PassengerEmail { get; set; } = string.Empty;
    public string PassengerPhone { get; set; } = string.Empty;
    public int NumberOfSeats { get; set; }
}
