using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SkyRoutes.Models;
public class Ticket
{
    public int Id { get; set; }
    public int? TechnicianId { get; set; }
    [ForeignKey("TechnicianId")]
    public UserProfile? Technician { get; set; }
    public int DroneId { get; set; }
    
    public Drone? Drone { get; set; }
    public int RepairTypeId { get; set; }
    public RepairType? RepairType { get; set; }
    public int SubmittedById { get; set; }
    public DateTime? InRepairSince { get; set; }
    public DateTime? OutOfRepair { get; set; } // Use DateTime? to allow for null (if the repair is ongoing)
    public string RepairSummary { get; set; }
    public bool Open { get; set; }
}