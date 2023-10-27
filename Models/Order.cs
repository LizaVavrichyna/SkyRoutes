using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SkyRoutes.Models;
public class Order
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Address { get; set; }
    public DateTime DeliveryDate { get; set; }
    public int? RouteId { get; set; }

    public Route? Route { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public bool Delivered { get; set; }
    public double? Distance { get; set; }
    public bool? IsHub { get; set; }
}