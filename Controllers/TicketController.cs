using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkyRoutes.Models;
using SkyRoutes.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace SkyRoutes.Controllers;

[ApiController]
[Route("api/[controller]")] //"/api/ticket" 

public class TicketController : ControllerBase
{
    private SkyRoutesDbContext _dbContext;

    public TicketController(SkyRoutesDbContext context, UserManager<IdentityUser> userManager)
    {
        _dbContext = context;

    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        //HTTP response with a status of 200, as well as the data that's passed in
        return Ok(_dbContext.Tickets
        .Include(t => t.RepairType)
        .Include(t => t.Technician)
        .ToList());
    }

    [HttpGet("{droneId}")]
    [Authorize]
    public IActionResult GetByDroneId(int droneId)
    {
        //HTTP response with a status of 200, as well as the data that's passed in
        return Ok(_dbContext.Tickets
        .Include(t => t.RepairType)
        .Include(t => t.Technician)
        .Where(t => t.DroneId == droneId)
        .ToList());
    }

    //post new ticket
    [HttpPost]
    [Authorize]
    public IActionResult Createticket(Ticket ticket)
    {
        ticket.InRepairSince = DateTime.Today;
        _dbContext.Tickets.Add(ticket);
        _dbContext.SaveChanges();
        return Created($"/api/ticket/{ticket.Id}", ticket);
    }

    //close ticket
    [HttpPut("close")]
    [Authorize]
    public IActionResult CloseTicket(Ticket ticket)
    {
        Ticket ticketToUpdate = _dbContext.Tickets.SingleOrDefault(d => d.Id == ticket.Id);
        if (ticketToUpdate == null)
        {
            return NotFound();
        }
        else if (ticketToUpdate.Id != ticket.Id)
        {
            return BadRequest();
        }
        ticketToUpdate.Open = false;
        ticketToUpdate.RepairSummary = ticket.RepairSummary;
        ticketToUpdate.OutOfRepair = DateTime.Today;
        _dbContext.SaveChanges();

        return Ok();
    }

    //assign ticket
    [HttpPut("assign")]
    [Authorize]
    public IActionResult AssignTicket(Ticket ticket)
    {
        Ticket ticketToUpdate = _dbContext.Tickets.SingleOrDefault(d => d.Id == ticket.Id);
        if (ticketToUpdate == null)
        {
            return NotFound();
        }
        else if (ticketToUpdate.Id != ticket.Id)
        {
            return BadRequest();
        }
        ticketToUpdate.TechnicianId = ticket.TechnicianId;
        _dbContext.SaveChanges();

        return Ok();
    }
}