using Microsoft.AspNetCore.Mvc;
using FlightBookingAPI.Models;
using FlightBookingAPI.Services;

namespace FlightBookingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>
    /// Get all bookings
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
    {
        var bookings = await _bookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    /// <summary>
    /// Get booking by reference
    /// </summary>
    [HttpGet("{reference}")]
    public async Task<ActionResult<Booking>> GetBooking(string reference)
    {
        var booking = await _bookingService.GetBookingAsync(reference);
        
        if (booking == null)
            return NotFound(new { message = "Booking not found" });
        
        return Ok(booking);
    }

    /// <summary>
    /// Create a new booking
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Booking>> CreateBooking([FromBody] BookingRequest request)
    {
        try
        {
            var booking = await _bookingService.CreateBookingAsync(request);
            return CreatedAtAction(nameof(GetBooking), new { reference = booking.BookingReference }, booking);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Cancel a booking
    /// </summary>
    [HttpDelete("{reference}")]
    public async Task<ActionResult<Booking>> CancelBooking(string reference)
    {
        var booking = await _bookingService.CancelBookingAsync(reference);
        
        if (booking == null)
            return NotFound(new { message = "Booking not found" });
        
        return Ok(booking);
    }
}
