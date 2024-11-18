using System;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleFly_BookingSystem.DTOs;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class FlightRouteController : ControllerBase
    {
        private readonly IFlightRouteService _flightRouteService;

        public FlightRouteController(IFlightRouteService flightRouteService)
        {
            _flightRouteService = flightRouteService;
        }

        [Authorize(Roles = "User,FlightOwner,Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<FlightRoute>> GetAll()
        {
            return Ok(_flightRouteService.GetAll());
        }

        [Authorize(Roles = "FlightOwner,Admin")]
        [HttpPost]
        public IActionResult AddFlightRoute([FromBody] FlightRouteDTO routeCreateDTO)
        {
            if (routeCreateDTO == null)
                return BadRequest("Invalid route data.");

            try
            {
                var createdRoute = _flightRouteService.AddRoute(routeCreateDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdRoute.Id }, createdRoute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "FlightOwner,Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateFlightRoute(int id, [FromBody] FlightRouteDTO routeUpdateDTO)
                {
                    if (routeUpdateDTO == null)
                        return BadRequest("Invalid route data.");

                    try
                    {
                        var updatedRoute = _flightRouteService.UpdateRoute(id, routeUpdateDTO);
                        if (updatedRoute == null)
                            return NotFound($"Route with id {id} not found.");

                        return Ok(updatedRoute);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

        [Authorize(Roles = "User,FlightOwner,Admin")]
        [HttpGet("{id}")]
        public ActionResult<FlightRoute> GetById(int id)
        {
            var route = _flightRouteService.GetById(id);
            if (route == null)
                return NotFound(new { Message = "Route not found." });

            return Ok(route);
        }

        [Authorize(Roles = "User,FlightOwner,Admin")]
        [HttpGet("search")]
        public ActionResult<IEnumerable<FlightRouteDTO>> SearchFlightRoutes(
        [FromQuery] string? origin,
        [FromQuery] string? destination,
        [FromQuery] DateTime? date,
        [FromQuery] decimal? minFare,
        [FromQuery] decimal? maxFare,
        [FromQuery] int? minAvailableSeats,
        [FromQuery] int? maxAvailableSeats)
        {
            try
            {
                var results = _flightRouteService.SearchRoutes(
                    origin,
                    destination,
                    date,
                    minFare,
                    maxFare,
                    minAvailableSeats,
                    maxAvailableSeats);

                if (!results.Any())
                    return NotFound(new { Message = "No routes found for the given search criteria." });

                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
