using System;
using SimpleFly_BookingSystem.DTOs;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Interfaces
{
	public interface IFlightRouteService
	{
        IEnumerable<FlightRouteDTO> GetAll();
        FlightRouteDTO GetById(int id);
        FlightRouteDTO AddRoute(FlightRouteDTO routeCreateDTO);
        FlightRouteDTO UpdateRoute(int id, FlightRouteDTO routeUpdateDTO);
        IEnumerable<FlightRouteDTO> SearchRoutes(string origin = null, string destination = null, DateTime? date = null, decimal? minFare = null, decimal? maxFare = null, int? minAvailableSeats = null, int? maxAvailableSeats = null);
    }
}