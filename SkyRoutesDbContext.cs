using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SkyRoutes.Models;
using Microsoft.AspNetCore.Identity;
using Route = SkyRoutes.Models.Route;

namespace SkyRoutes.Data;
public class SkyRoutesDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Drone> Drones { get; set; }
    public DbSet<RepairType> RepairTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public SkyRoutesDbContext(DbContextOptions<SkyRoutesDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Generate unique Guid IDs for roles and users
        string adminRoleId = Guid.NewGuid().ToString();
        string technicianRoleId = Guid.NewGuid().ToString();
        string expeditorRoleId = Guid.NewGuid().ToString();

        // Add IdentityRoles "Admin," "Technician," and "Expeditor"
        //holds the various roles that a use can have
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[]
        {
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "admin"
            },
            new IdentityRole
            {
                Id = technicianRoleId,
                Name = "Technician",
                NormalizedName = "technician"
            },
            new IdentityRole
            {
                Id = expeditorRoleId,
                Name = "Expeditor",
                NormalizedName = "expeditor"
            }
        });

        //holds login credentials for users
        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser[]
        {
            new IdentityUser
            {
                Id = "abc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                UserName = "Administrator",
                Email = "admina@skyroutes.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "t8d76512-74f1-43bb-b1fd-87d3a8aa36df",
                UserName = "JohnDoe",
                Email = "john@skyroutes.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "t7d21fac-3b21-454a-a747-075f072d0cf3",
                UserName = "JaneSmith",
                Email = "jane@skyroutes.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "t806cfae-bda9-47c5-8473-dd52fd056a9b",
                UserName = "AliceJohnson",
                Email = "alice@skyroutes.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "ece89d88-75da-4a80-9b0d-3fe58582b8e2",
                UserName = "BobWilliams",
                Email = "bob@skyroutes.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "e224a03d-bf0c-4a05-b728-e3521e45d74d",
                UserName = "EveDavis",
                Email = "Eve@skyroutes.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },

        });

        //defines which users have which roles
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = "abc40bc6-0829-4ac5-a3ed-180f5e916a5f"
            },
            new IdentityUserRole<string>
            {
                RoleId = technicianRoleId,
                UserId = "t8d76512-74f1-43bb-b1fd-87d3a8aa36df"
            },
            new IdentityUserRole<string>
            {
                RoleId = technicianRoleId,
                UserId = "t7d21fac-3b21-454a-a747-075f072d0cf3"
            },
            new IdentityUserRole<string>
            {
                RoleId = technicianRoleId,
                UserId = "t806cfae-bda9-47c5-8473-dd52fd056a9b"
            },
            new IdentityUserRole<string>
            {
                RoleId = expeditorRoleId,
                UserId = "ece89d88-75da-4a80-9b0d-3fe58582b8e2"
            },
            new IdentityUserRole<string>
            {
                RoleId = expeditorRoleId,
                UserId = "e224a03d-bf0c-4a05-b728-e3521e45d74d"
            },

        });

        modelBuilder.Entity<UserProfile>().HasData(new UserProfile[]
        {
            new UserProfile
            {
                Id = 1,
                IdentityUserId = "abc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                FirstName = "Admin",
                LastName = "Maverick",
                CreateDateTime = new DateTime(2022, 1, 25),
                IsActive = true
            },
             new UserProfile
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                CreateDateTime = new DateTime(2023, 2, 2),
                IdentityUserId = "t8d76512-74f1-43bb-b1fd-87d3a8aa36df",
                IsActive = true
            },
            new UserProfile
            {
                Id = 3,
                FirstName = "Jane",
                LastName = "Smith",
                CreateDateTime = new DateTime(2022, 3, 15),
                
                IdentityUserId = "t7d21fac-3b21-454a-a747-075f072d0cf3",
                IsActive = true
            },
            new UserProfile
            {
                Id = 4,
                FirstName = "Alice",
                LastName = "Johnson",
                CreateDateTime = new DateTime(2023, 6, 10),
                
                IdentityUserId = "t806cfae-bda9-47c5-8473-dd52fd056a9b",
                IsActive = true
            },
            new UserProfile
            {
                Id = 5,
                FirstName = "Bob",
                LastName = "Williams",
                CreateDateTime = new DateTime(2023, 5, 15),
               
                IdentityUserId = "ece89d88-75da-4a80-9b0d-3fe58582b8e2",
                IsActive = true
            },
            new UserProfile
            {
                Id = 6,
                FirstName = "Eve",
                LastName = "Davis",
                CreateDateTime = new DateTime(2022, 10, 18),
               
                IdentityUserId = "e224a03d-bf0c-4a05-b728-e3521e45d74d",
                IsActive = true
            }
        });
        
        modelBuilder.Entity<Drone>().HasData(new Drone[]
        {
            new Drone
            {
                Id = 1,
                Model = "Phantom X1",
                Callsign = "PX1",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 5000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2021, 5, 15),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 2,
                Model = "Mavic Air 2",
                Callsign = "MA2",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 8000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2020, 11, 10),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 3,
                Model = "Inspire 2",
                Callsign = "I2",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 10000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2019, 7, 20),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 4,
                Model = "Parrot Anafi",
                Callsign = "PA",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 2000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2020, 3, 5),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 5,
                Model = "Autel EVO Lite+",
                Callsign = "EVO+",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 15000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2018, 12, 3),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 6,
                Model = "DJI Mini 2",
                Callsign = "Mini2",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 3000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2020, 8, 9),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 7,
                Model = "Yuneec Typhoon H Pro",
                Callsign = "Typhoon",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 12000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2019, 2, 15),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 8,
                Model = "Skydio 2",
                Callsign = "S2",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 5000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2021, 8, 30),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 9,
                Model = "Autel EVO Nano",
                Callsign = "Nano",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 1000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2022, 4, 11),
                IsActive = true,
                InHangar = true
            },
            new Drone
            {
                Id = 10,
                Model = "FreeFly Alta 6",
                Callsign = "Alta6",
                ImageLocation = "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg",
                DistanceCap = 18000, // Set an appropriate value for the distance capability in meters
                InFleetSince = new DateTime(2019, 10, 5),
                IsActive = true,
                InHangar = true
            }
        });

        // Seed data for the RepairType entity
        modelBuilder.Entity<RepairType>().HasData(new RepairType[]
        {
            new RepairType { Id = 1, Name = "Propeller Replacement" },
            new RepairType { Id = 2, Name = "Battery Connector Repair" },
            new RepairType { Id = 3, Name = "Gimbal Stabilization" },
            new RepairType { Id = 4, Name = "Motor Replacement" },
            new RepairType { Id = 5, Name = "GPS Module Troubleshooting" },
            new RepairType { Id = 6, Name = "Camera Sensor Cleaning" },
            new RepairType { Id = 7, Name = "ESC Calibration" },
            new RepairType { Id = 8, Name = "Controller Calibration" },
            new RepairType { Id = 9, Name = "Frame Repair or Replacement" },
            new RepairType { Id = 10, Name = "Firmware Updates" }
        });

        //order seeding 
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order
            {
                Id = 123,
                Address = "50 Airways Blvd, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(2),
                RouteId = 1,
                Longitude = -86.6919461,
                Latitude = 36.1300431,
                Delivered = false,
                IsHub = true
            },
            //not delivered
            //have route
            new Order
            {
                Id = 1,
                Address = "123 Main St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(2),
                RouteId = 1,
                Longitude = -86.7816,
                Latitude = 36.1627,
                Delivered = false
            },
            new Order
            {
                Id = 2,
                Address = "456 Elm St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(2),
                RouteId = 1,
                Longitude = -86.7816,
                Latitude = 36.1563,
                Delivered = false
            },
            new Order
            {
                Id = 3,
                Address = "789 Oak St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(2),
                RouteId = 1,
                Longitude = -86.7845,
                Latitude = 36.1542,
                Delivered = false
            },
            //delivered
            //route 2
            new Order
            {
                Id = 4,
                Address = "1055 Pine St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-1),
                RouteId = 2,
                Longitude = -86.7825589,
                Latitude = 36.1524522,
                Delivered = true 
            },
            new Order
            {
                Id = 5,
                Address = "610 Birch Glen Ct, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-1),
                RouteId = 2,
                Longitude = -86.96920420000001,
                Latitude = 36.0718966,
                Delivered = true 
            },
            new Order
            {
                Id = 6,
                Address = "400 Maple St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-1),
                RouteId = 2,
                Longitude = -86.7051051,
                Latitude = 36.2615156,
                Delivered = true 
            },
            //not delivered
            //no route
            new Order
            {
                Id = 7,
                Address = "548 Cedar St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(8),
                
                Longitude = -86.70654039999999,
                Latitude = 36.0445029,
                Delivered = false
            },
            new Order
            {
                Id = 8,
                Address = "827 Redwood St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(8),
                
                Longitude = -86.7801446,
                Latitude = 36.0563849,
                Delivered = false
            },
            new Order
            {
                Id = 9,
                Address = "615 Spruce St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(8),
                
                Longitude = -86.80638239999999,
                Latitude = 36.1596594,
                Delivered = false
            },
            new Order
            {
                Id = 10,
                Address = "2929 Willow St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(8),
                
                Longitude = -86.7531091,
                Latitude = 36.1547001,
                Delivered = false
            },
            new Order
            {
                Id = 11,
                Address = "3232 Sycamore Ln, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(8),
               
                Longitude = -86.8293247,
                Latitude = 36.1129177,
                Delivered = false
            },

            //delivered
            //route 3
            new Order
            {
                Id = 12,
                Address = "3535 Juniper St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-2),
                RouteId = 3,
                Longitude = -86.7810,
                Latitude = 36.1616,
                Delivered = true 
            },
            new Order
            {
                Id = 13,
                Address = "3838 Cedar Elm St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-2),
                RouteId = 3,
                Longitude = -86.7704,
                Latitude = 36.1678,
                Delivered = true 
            },
            new Order
            {
                Id = 14,
                Address = "4141 Red Oak St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-2),
                RouteId = 3,
                Longitude = -86.7841,
                Latitude = 36.1597,
                Delivered = true 
            },
            new Order
            {
                Id = 15,
                Address = "4444 Dogwood St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-2),
                RouteId = 3,
                Longitude = -86.7775,
                Latitude = 36.1634,
                Delivered = true 
            },
            new Order
            {
                Id = 16,
                Address = "4747 Chestnut St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-2),
                RouteId = 3,
                Longitude = -86.7753,
                Latitude = 36.1568,
                Delivered = true 
            },
            new Order
            {
                Id = 17,
                Address = "5050 Willow Oak St, Nashville, TN",
                DeliveryDate = DateTime.Now.AddDays(-2),
                RouteId = 3,
                Longitude = -86.7812,
                Latitude = 36.1671,
                Delivered = true 
            }
        });

        modelBuilder.Entity<Route>().HasData(new Route[]
        {
            new Route
            {
                Id = 1,
                DroneId = 1,   
                DeliveredOn = new DateTime(2023, 10,21),            
                ExpeditorId = 6
            },
            new Route
            {
                Id = 2,
                DroneId = 2,
                DeliveredOn = new DateTime(2023, 10,21),
                ExpeditorId = 5
            },
            new Route
            {
                Id = 3,
                DroneId = 3,  
                DeliveredOn = new DateTime(2023, 10,21),           
                ExpeditorId = 6
            }
        });

        modelBuilder.Entity<Ticket>().HasData(new Ticket[]
        {   
            new Ticket
            {
                Id = 1,
                TechnicianId = 4,
                DroneId = 1,
                RepairTypeId = 1,
                SubmittedById = 6,
                InRepairSince = new DateTime(2023, 9,28),
                OutOfRepair = new DateTime(2023, 9,30),
                RepairSummary = "Simple propeller blade repl",
                Open = false
            }

        });

        
    }
}