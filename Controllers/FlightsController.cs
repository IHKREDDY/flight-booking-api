using Microsoft.AspNetCore.Mvc;
using FlightBookingAPI.Models;
using FlightBookingAPI.Services;

namespace FlightBookingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    /// <summary>
    /// Get all available flights
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
    {
        var flights = await _flightService.GetAllFlightsAsync();
        return Ok(flights);
    }

    /// <summary>
    /// Get flight by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Flight>> GetFlight(int id)
    {
        var flight = await _flightService.GetFlightByIdAsync(id);
        
        if (flight == null)
            return NotFound();
        
        return Ok(flight);
    }

    /// <summary>
    /// Search for flights
    /// </summary>
    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<Flight>>> SearchFlights([FromBody] FlightSearchRequest request)
    {
        var flights = await _flightService.SearchFlightsAsync(request);
        return Ok(flights);
    }
}
