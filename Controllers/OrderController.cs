using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkyRoutes.Models;
using SkyRoutes.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


namespace SkyRoutes.Controllers;

[ApiController]
[Route("api/[controller]")] //"/api/order" 

public class OrderController : ControllerBase
{
    private SkyRoutesDbContext _dbContext;

    private IConfiguration _configuration;
    public OrderController(SkyRoutesDbContext context, UserManager<IdentityUser> userManager,IConfiguration config)
    {
        _dbContext = context;
        _configuration = config;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        //HTTP response with a status of 200, as well as the data that's passed in
        return Ok(_dbContext.Orders.OrderBy(o => o.DeliveryDate).Where(o => o.Delivered == false).ToList());
    }
    
    //get order by id
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        Order order = _dbContext
            .Orders
            .Include(o => o.Route)
            .ThenInclude(r => r.Drone)
            .Include(o => o.Route)
            .ThenInclude(r => r.Expeditor)
            .SingleOrDefault(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }
        
        return Ok(order);
    }


    [HttpPost("")]
    [Authorize]
    public IActionResult CreateOrder(Order order)
    {

            string GoogleMapsApiKey = _configuration["GoogleMapsApiKey"];
            string requestUri = $"https://maps.googleapis.com/maps/api/geocode/json?address={order.Address}&key={GoogleMapsApiKey}";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = httpClient.GetAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                // Create a JSON document from the response
                    using (JsonDocument document = JsonDocument.Parse(jsonResponse))
                    {
                        // Access the root of the JSON document
                        JsonElement root = document.RootElement;
                        
                                        // Check if the 'results' array is empty
                    if (root.TryGetProperty("results", out JsonElement resultsArray) && resultsArray.GetArrayLength() > 0)
                    {
                        // Access the first result
                        JsonElement firstResult = resultsArray[0];

                        // Access the 'geometry' element within the result
                        JsonElement geometry = firstResult.GetProperty("geometry");

                        JsonElement address = firstResult.GetProperty("formatted_address");
                        // Access the 'location' element within the geometry
                        JsonElement location = geometry.GetProperty("location");

                        // Extract latitude and longitude
                        double lat = location.GetProperty("lat").GetDouble();
                        double lng = location.GetProperty("lng").GetDouble();
                        Order newOrder = new Order
                        {
                            Address = address.ToString(),
                            DeliveryDate = order.DeliveryDate,
                            Latitude = lat,
                            Longitude = lng,
                            Delivered = false
                        };

                        // Add the new order object to the database and save the changes.
                        _dbContext.Orders.Add(newOrder);
                        _dbContext.SaveChanges();

                        // Return a success response with the new order object.
                        return Created($"/api/order/{newOrder.Id}", newOrder);
                    }
                    else
                    {
                        // No results found in the response, return a BadRequest
                        return BadRequest($"{order.Address}");
                    }
                    }
            }  else
            {
                return BadRequest("Error");
            }

        }
    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteOrder( int id)
    {
        Order OrderToDelete = _dbContext.Orders.SingleOrDefault(pt => pt.Id == id);
        if (OrderToDelete == null)
        {
            return NotFound();
        }
        else if (id != OrderToDelete.Id)
        {
            return BadRequest();
        }
        
        _dbContext.Orders.Remove(OrderToDelete);
        _dbContext.SaveChanges();

        return NoContent();
    }

    //update order
    [HttpPut("order.id")]
    [Authorize]
    public IActionResult UpdateDrone(Order order)
    {
        Order orderToUpdate = _dbContext.Orders.SingleOrDefault(d => d.Id == order.Id);
        if (orderToUpdate == null)
        {
            return NotFound();
        }
        else if (orderToUpdate.Id != order.Id)
        {
            return BadRequest();
        }
        orderToUpdate.Address = order.Address;
        _dbContext.SaveChanges();

        return Ok();
    }
}