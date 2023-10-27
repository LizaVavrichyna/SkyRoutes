using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkyRoutes.Models;
using SkyRoutes.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using SkyRoutes.Models;
using Route = SkyRoutes.Models.Route;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace SkyRoutes.Controllers;

[ApiController]
[Route("api/[controller]")] //"/api/route" 

public class RouteController : ControllerBase
{
    private SkyRoutesDbContext _dbContext;

    private IConfiguration _configuration;
    public RouteController(SkyRoutesDbContext context, UserManager<IdentityUser> userManager,IConfiguration config)
    {
        _dbContext = context;
        _configuration = config;
    }
        private double CalculateDistance(Order Start, Order End)
    {
        double latitude1 = Start.Latitude * Math.PI / 180;
        double longitude1 = Start.Longitude * Math.PI / 180;
        double latitude2 = End.Latitude * Math.PI / 180;
        double longitude2 = End.Longitude * Math.PI / 180;

        double dLatitude = latitude2 - latitude1;
    double dLongitude = longitude2 - longitude1;
        // Calculate the distance using the great circle formula.
        double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2) + Math.Cos(latitude1) * Math.Cos(latitude2) * Math.Pow(Math.Sin(dLongitude / 2.0), 2);
    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        // Return the distance in kilometers.
        return c * 6371.0;
    }

    private List<Location> Dijkstra(List<Location> locations, Location hub)
    {
        // Initialize distances to infinity and the hub's distance to 0.
        foreach (var location in locations)
        {
            location.DistanceFromHub = double.PositiveInfinity;
        }
        hub.DistanceFromHub = 0;

        // Create a set to keep track of unvisited locations.
        var unvisited = new HashSet<Location>(locations);
        List<Location> efficientRoute = new List<Location>();
        while (unvisited.Count > 0)
        {
            //get the closest order to the hub
            var current = unvisited.OrderBy(l => l.DistanceFromHub).First();

            if (efficientRoute.Count > 0)
        {
            // Check if adding this location to the route results in a shorter path to the hub.
            var distanceToHub = CalculateDistance(_dbContext.Orders.FirstOrDefault(o => o.Id == current.Id), _dbContext.Orders.FirstOrDefault(o => o.Id == hub.Id));
            if (current.DistanceFromHub > efficientRoute.Last().DistanceFromHub + distanceToHub)
            {
                // If a shorter path is found, remove locations from the end of the route until this one.
                while (efficientRoute.Last() != current)
                {
                    unvisited.Add(efficientRoute.Last());
                    efficientRoute.RemoveAt(efficientRoute.Count - 1);
                }
            }
        }

            //remove found order from the set of stops
            unvisited.Remove(current);
            efficientRoute.Add(current); // Add the current location to the route.
            foreach (var neighbor in current.Neighbors)
            {
                //get the distance from (current stop and the hub) plus (distance between current stop and the neighbor)
                var distance = current.DistanceFromHub + CalculateDistance(_dbContext.Orders.FirstOrDefault(o => o.Id == current.Id), _dbContext.Orders.FirstOrDefault(o => o.Id == neighbor.Id));
                //if found distance is less then neighbor's distnace from the hub
                if (distance < neighbor.DistanceFromHub)
                {
                    
                    neighbor.DistanceFromHub = distance;
                }
            }
            
        }

        // Sort the locations by distance from the hub.
        efficientRoute = locations.OrderBy(l => l.DistanceFromHub).ToList();
        return efficientRoute;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        //HTTP response with a status of 200, as well as the data that's passed in
        return Ok(_dbContext.Routes
        .Include(r => r.Orders)
        .Include(r => r.Expeditor)
        .Include(r => r.Drone)
        .OrderByDescending(r => r.Id)
        .ToList());
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post(Route route)
    {

        if (route.Orders == null || route.Orders.Count == 0)
        {
            return BadRequest("No order objects provided.");
        }
        //find the hub
        Order hub = _dbContext.Orders.FirstOrDefault(o => o.Id == 123);
        //
        var locations = new List<Location>();

        // Create Location objects for each order in route.Orders and add the hub.
        foreach (var order in route.Orders)
        {
            var location = new Location
            {
                Id = order.Id,
                Latitude = order.Latitude,
                Longitude = order.Longitude,
                Neighbors = new List<Location>(),
                DistanceFromHub = CalculateDistance(hub, order)
            };
            
            locations.Add(location);
        }

        // Add the hub as a location.
        Location hubLocation = new Location
        {
            Id = hub.Id,
            Latitude = hub.Latitude,
            Longitude = hub.Longitude,
            Neighbors = new List<Location>(),
            DistanceFromHub = 0 // Distance from the hub to itself is 0.
        };
        // Calculate the most efficient route.
        var efficientRoute = Dijkstra(locations, hubLocation);

        // Extract the ordered orders from the efficient route.
        var orderedOrderIds = efficientRoute.Select(location => location.Id).ToList();
        var orderedOrders = _dbContext.Orders.Where(order => orderedOrderIds.Contains(order.Id)).ToList();
        foreach (Order order in orderedOrders)
        {
            order.Delivered = true;
            order.RouteId = route.Id;
        }
        route.Orders = orderedOrders;
        route.DeliveredOn = DateTime.Today;

        // Save the new route to the database.
        _dbContext.Routes.Add(route);
        _dbContext.SaveChanges();

        return Created($"/api/route/{route.Id}", route);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteRoute( int id)
    {
        Route RouteToDelete = _dbContext.Routes.SingleOrDefault(pt => pt.Id == id);
        if (RouteToDelete == null)
        {
            return NotFound();
        }
        else if (id != RouteToDelete.Id)
        {
            return BadRequest();
        }
        foreach(Order o in _dbContext.Orders)
        {
            if(o.RouteId == id)
            {
                o.RouteId = null;
                o.Route = null;
                o.Delivered = false;
            }
        }
        _dbContext.Routes.Remove(RouteToDelete);

        _dbContext.SaveChanges();

        return NoContent();
    }
}