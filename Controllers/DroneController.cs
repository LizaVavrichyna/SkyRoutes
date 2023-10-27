using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkyRoutes.Models;
using SkyRoutes.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace SkyRoutes.Controllers;

[ApiController]
[Route("api/[controller]")] //"/api/drone" 

public class DroneController : ControllerBase
{
    private SkyRoutesDbContext _dbContext;

    public DroneController(SkyRoutesDbContext context, UserManager<IdentityUser> userManager)
    {
        _dbContext = context;

    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        //HTTP response with a status of 200, as well as the data that's passed in
        return Ok(_dbContext.Drones.ToList());
    }
    [HttpGet("active")]
    [Authorize]
    public IActionResult GetActive()
    {
        //HTTP response with a status of 200, as well as the data that's passed in
        return Ok(_dbContext.Drones.Where(d => d.IsActive == true).ToList());
    }
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        return Ok(_dbContext.Drones.SingleOrDefault(d => d.Id == id));
    }

    //drone soft delete
    [HttpPut]
    [Authorize]
    public IActionResult UpdateDrone(Drone drone)
    {
        Drone droneToUpdate = _dbContext.Drones.SingleOrDefault(d => d.Id == drone.Id);
        if (droneToUpdate == null)
        {
            return NotFound();
        }
        else if (droneToUpdate.Id != drone.Id)
        {
            return BadRequest();
        }
        droneToUpdate.IsActive = false;
        droneToUpdate.InHangar = true;
        _dbContext.SaveChanges();

        return Ok();
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateDrone(Drone drone)
    {
        drone.InFleetSince = DateTime.Today;
        _dbContext.Drones.Add(drone);
        _dbContext.SaveChanges();
        return Created($"/api/Drone/{drone.Id}", drone);
    }
}