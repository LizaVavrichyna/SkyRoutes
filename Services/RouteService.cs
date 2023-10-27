using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SkyRoutes.Data;
using SkyRoutes.Models;

public class RouteService
{
    private readonly SkyRoutesDbContext _dbContext;

    public RouteService(SkyRoutesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Method to calculate the distance between two orders (Haversine formula).
    
    private double CalculateDistance(Order Start, Order End)
    {
        double latitudeStartInRadians = Start.Latitude * (Math.PI / 180);
        double longitudeStartInRadians = Start.Longitude * (Math.PI / 180);
        double latitudeEndInRadians = End.Latitude * (Math.PI / 180);
        double longitudeEndInRadians = End.Longitude * (Math.PI / 180);

        double dlon = longitudeEndInRadians - longitudeStartInRadians;
        double dlat = latitudeEndInRadians - latitudeStartInRadians;
        double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(latitudeStartInRadians) * Math.Cos(latitudeEndInRadians) * Math.Pow(Math.Sin(dlon / 2), 2);
        double c = 2 * Math.Asin(Math.Sqrt(a));
        //distance in km 
        return c;
    }
    
    // Method to find the closest order to the hub for a specific route.
    public List<Order> FindClosestOrder(int routeId)
    {
        // Get the orders for the specified route and hub.
        var ordersForRoute = _dbContext.Orders
            .Where(o => o.RouteId == routeId)
            .ToList();

        // Find the hub's location
        var hub = ordersForRoute.FirstOrDefault(ro => ro.IsHub == true);

        if (hub == null)
        {
            // Handle the case where no hub is found for the route.
            return null;
        }

        // Create a list to store the ordered stops.
        List<Order> orderedStops = new List<Order>();

        while (ordersForRoute.Count > 0)
        {
            // Calculate the distances to all remaining orders from the current hub.
            Dictionary<Order, double> distances = new Dictionary<Order, double>();
            foreach (var order in ordersForRoute)
            {
                if (order.Id != hub.Id) // Exclude the hub itself
                {
                    double distance = CalculateDistance(hub, order);
                    distances[order] = distance;
                }
            }

            // Find the order with the shortest remaining distance.
            var nextOrder = distances.OrderBy(d => d.Value).FirstOrDefault().Key;

            // Add the next closest order to the list.
            orderedStops.Add(nextOrder);

            // Update the current hub to the newly found order.
            hub = nextOrder;

            // Remove the added order from the list of orders.
            ordersForRoute.Remove(nextOrder);
        }

        return orderedStops;
    }
    
    
}