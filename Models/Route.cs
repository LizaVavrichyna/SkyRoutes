using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SkyRoutes.Models;
public class Route
{
    public int Id { get; set; }
    public int DroneId { get; set; }
    public Drone? Drone { get; set; }
    
    public int ExpeditorId { get; set; }
    [ForeignKey("ExpeditorId")]
    public UserProfile? Expeditor { get; set; }
    public DateTime? DeliveredOn { get; set; }
    public List<Order>? Orders { get; set; }
 
}