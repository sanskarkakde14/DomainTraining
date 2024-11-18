using SimpleFly_BookingSystem.Data;
using SimpleFly_BookingSystem.DTOs;
using Microsoft.EntityFrameworkCore;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.Models;

public class FlightRouteService : IFlightRouteService
{
    private readonly SimplyFlyContext _context;

    public FlightRouteService(SimplyFlyContext context)
    {
        _context = context;
    }

    public IEnumerable<FlightRouteDTO> GetAll()
    {
        return _context.FlightRoutes
            .Include(fr => fr.FlightOwner)
            .Select(fr => new FlightRouteDTO
            {
                Id = fr.Id,
                Origin = fr.Origin,
                Destination = fr.Destination,
                Date = fr.Date,
                Fare = fr.Fare,
                TotalSeats = fr.TotalSeats,
                AvailableSeats = fr.AvailableSeats,
                FlightOwnerUsername = fr.FlightOwner.Username 
            })
            .ToList();
    }

    public FlightRouteDTO GetById(int id)
    {
        var route = _context.FlightRoutes
            .Include(fr => fr.FlightOwner)
            .FirstOrDefault(fr => fr.Id == id);

        if (route == null) return null;

        return new FlightRouteDTO
        {
            Id = route.Id,
            Origin = route.Origin,
            Destination = route.Destination,
            Date = route.Date,
            Fare = route.Fare,
            TotalSeats = route.TotalSeats,
            AvailableSeats = route.AvailableSeats,
            FlightOwnerUsername = route.FlightOwner.Username
        };
    }

    
    public FlightRouteDTO AddRoute(FlightRouteDTO routeCreateDTO)
    {
        if (routeCreateDTO.TotalSeats <= 0)
            throw new ArgumentException("TotalSeats must be a positive number.");

        int availableSeats = routeCreateDTO.AvailableSeats <= routeCreateDTO.TotalSeats ? routeCreateDTO.AvailableSeats : routeCreateDTO.TotalSeats;

        var flightOwner = _context.Users.FirstOrDefault(u => u.Username == routeCreateDTO.FlightOwnerUsername);
        if (flightOwner == null)
            throw new ArgumentException("Invalid Flight Owner Username.");

        var route = new FlightRoute
        {
            Origin = routeCreateDTO.Origin,
            Destination = routeCreateDTO.Destination,
            Date = routeCreateDTO.Date,
            Fare = routeCreateDTO.Fare,
            TotalSeats = routeCreateDTO.TotalSeats,
            AvailableSeats = availableSeats,
            FlightOwner = flightOwner 
        };

        _context.FlightRoutes.Add(route);
        _context.SaveChanges();

        return new FlightRouteDTO
        {
            Id = route.Id,
            Origin = route.Origin,
            Destination = route.Destination,
            Date = route.Date,
            Fare = route.Fare,
            TotalSeats = route.TotalSeats,
            AvailableSeats = route.AvailableSeats,
            FlightOwnerUsername = route.FlightOwner.Username 
        };
    }

    public FlightRouteDTO UpdateRoute(int id, FlightRouteDTO routeCreateDTO)
    {
        if (routeCreateDTO.TotalSeats <= 0)
            throw new ArgumentException("TotalSeats must be a positive number.");

        int availableSeats = routeCreateDTO.AvailableSeats <= routeCreateDTO.TotalSeats ? routeCreateDTO.AvailableSeats : routeCreateDTO.TotalSeats;

        var existingRoute = _context.FlightRoutes.Include(fr => fr.FlightOwner).FirstOrDefault(fr => fr.Id == id);
        if (existingRoute == null)
            throw new ArgumentException("Flight route not found.");
 
        var flightOwner = _context.Users.FirstOrDefault(u => u.Username == routeCreateDTO.FlightOwnerUsername);
        if (flightOwner == null)
            throw new ArgumentException("Invalid Flight Owner Username.");
        
        existingRoute.Origin = routeCreateDTO.Origin;
        existingRoute.Destination = routeCreateDTO.Destination;
        existingRoute.Date = routeCreateDTO.Date;
        existingRoute.Fare = routeCreateDTO.Fare;
        existingRoute.TotalSeats = routeCreateDTO.TotalSeats;
        existingRoute.AvailableSeats = availableSeats;
        existingRoute.FlightOwner = flightOwner; 

        _context.FlightRoutes.Update(existingRoute);
        _context.SaveChanges();

        return new FlightRouteDTO
        {
            Id = existingRoute.Id,
            Origin = existingRoute.Origin,
            Destination = existingRoute.Destination,
            Date = existingRoute.Date,
            Fare = existingRoute.Fare,
            TotalSeats = existingRoute.TotalSeats,
            AvailableSeats = existingRoute.AvailableSeats,
            FlightOwnerUsername = existingRoute.FlightOwner.Username 

        };
        }
        public IEnumerable<FlightRouteDTO> SearchRoutes(string? origin = null, string? destination = null, DateTime? date = null, decimal? minFare = null, decimal? maxFare = null, int? minAvailableSeats = null, int? maxAvailableSeats = null)
        {
            var query = _context.FlightRoutes.Include(fr => fr.FlightOwner).AsQueryable();

            if (!string.IsNullOrEmpty(origin))
                query = query.Where(fr => fr.Origin.Contains(origin));

            if (!string.IsNullOrEmpty(destination))
                query = query.Where(fr => fr.Destination.Contains(destination));

            if (date.HasValue)
                query = query.Where(fr => fr.Date.Date == date.Value.Date); 

            if (minFare.HasValue)
                query = query.Where(fr => fr.Fare >= minFare);

            if (maxFare.HasValue)
                query = query.Where(fr => fr.Fare <= maxFare);

            if (minAvailableSeats.HasValue)
                query = query.Where(fr => fr.AvailableSeats >= minAvailableSeats);

            if (maxAvailableSeats.HasValue)
                query = query.Where(fr => fr.AvailableSeats <= maxAvailableSeats);

            // Project the results into FlightRouteDTO
            return query.Select(fr => new FlightRouteDTO
            {
                Id = fr.Id,
                Origin = fr.Origin,
                Destination = fr.Destination,
                Date = fr.Date,
                Fare = fr.Fare,
                TotalSeats = fr.TotalSeats,
                AvailableSeats = fr.AvailableSeats,
                FlightOwnerUsername = fr.FlightOwner.Username
            }).ToList();
        }
}