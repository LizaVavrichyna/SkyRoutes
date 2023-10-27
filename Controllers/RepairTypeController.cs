using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkyRoutes.Models;
using SkyRoutes.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace SkyRoutes.Controllers;

[ApiController]
[Route("api/[controller]")] //"/api/repairType" 

public class RepairTypeController : ControllerBase
{
    private SkyRoutesDbContext _dbContext;

    public RepairTypeController(SkyRoutesDbContext context, UserManager<IdentityUser> userManager)
    {
        _dbContext = context;

    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        //HTTP response with a status of 200, as well as the data that's passed in
        return Ok(_dbContext.RepairTypes.ToList());
    }
}