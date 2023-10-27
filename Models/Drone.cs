using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SkyRoutes.Models;
public class Drone
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Model { get; set; }
    [Required]
    [MaxLength(50)]
    public string Callsign { get; set; }
    
    [DataType(DataType.Url)]
    [MaxLength(255)]
    public string ImageLocation { get; set; }
    
    public int DistanceCap { get; set; }
    public DateTime? InFleetSince { get; set; }
    public bool IsActive { get; set; }
    public bool InHangar { get; set; }
}