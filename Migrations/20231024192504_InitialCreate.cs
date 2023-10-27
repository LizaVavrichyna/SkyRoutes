using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkyRoutes.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Callsign = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageLocation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DistanceCap = table.Column<int>(type: "integer", nullable: false),
                    InFleetSince = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    InHangar = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdentityUserId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DroneId = table.Column<int>(type: "integer", nullable: false),
                    ExpeditorId = table.Column<int>(type: "integer", nullable: false),
                    DeliveredOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Drones_DroneId",
                        column: x => x.DroneId,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Routes_UserProfiles_ExpeditorId",
                        column: x => x.ExpeditorId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TechnicianId = table.Column<int>(type: "integer", nullable: true),
                    DroneId = table.Column<int>(type: "integer", nullable: false),
                    RepairTypeId = table.Column<int>(type: "integer", nullable: false),
                    SubmittedById = table.Column<int>(type: "integer", nullable: false),
                    InRepairSince = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OutOfRepair = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RepairSummary = table.Column<string>(type: "text", nullable: false),
                    Open = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Drones_DroneId",
                        column: x => x.DroneId,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_RepairTypes_RepairTypeId",
                        column: x => x.RepairTypeId,
                        principalTable: "RepairTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_UserProfiles_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RouteId = table.Column<int>(type: "integer", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Delivered = table.Column<bool>(type: "boolean", nullable: false),
                    Distance = table.Column<double>(type: "double precision", nullable: true),
                    IsHub = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "205f69b3-c8dc-4e19-a813-ed3608226ced", null, "Expeditor", "expeditor" },
                    { "58eeb428-a7c2-4919-adf7-5aecc885932d", null, "Admin", "admin" },
                    { "686c585a-525e-4938-a8a4-0669f463a7d7", null, "Technician", "technician" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "abc40bc6-0829-4ac5-a3ed-180f5e916a5f", 0, "89e64aea-3ab1-448a-87ad-e22f691d3cc1", "admina@skyroutes.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEAZ1xYXjCurvpCK+TDJClxRpIQ45/xUowHqlwb4Xe98ZEWaF/P189e8skOIhizlS0w==", null, false, "6c88d77c-9b86-4ab6-9880-a9cd6358fd74", false, "Administrator" },
                    { "e224a03d-bf0c-4a05-b728-e3521e45d74d", 0, "3b45e20a-0450-4a8a-85f6-bf63a28589c1", "Eve@skyroutes.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEKaKDVY+xVlHgbxYp8acDa8fnApFxKDn7V5alx30By5xM9XG2vzpE4WaFrBjkGelQQ==", null, false, "661dc833-05de-4fef-81df-6328f285dacb", false, "EveDavis" },
                    { "ece89d88-75da-4a80-9b0d-3fe58582b8e2", 0, "3f30109a-4a5f-4d06-a005-718cb10c5135", "bob@skyroutes.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEBCL1V3vIRb4T8KKRUbayRuczq5gJHotUDMyD7q3Lm53yaDlYzz3Y2bLJaitlwGISw==", null, false, "24e2fd82-353c-4869-9d3d-a118120cfcb3", false, "BobWilliams" },
                    { "t7d21fac-3b21-454a-a747-075f072d0cf3", 0, "33ef9196-f65a-4f5c-8a5c-7a5afaf8e9cb", "jane@skyroutes.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEOspPdc4FWXt6lbln+h0czoVeuzC+GcxJd3PCLToFv5kGV4DG5VorUQtymYVVQOqhg==", null, false, "7dcb28ff-6d0f-4849-8530-45719c379c9f", false, "JaneSmith" },
                    { "t806cfae-bda9-47c5-8473-dd52fd056a9b", 0, "7ddcf358-e7cf-40d5-963b-740d60425ab1", "alice@skyroutes.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEM6F+OatT1F2s4casUlQioJRUdCuEbMRyPzNUP3Ka/2uBYPuMvnujzd+bs6MTJLxIw==", null, false, "285cf006-427f-4680-8d03-5a958cc2dfef", false, "AliceJohnson" },
                    { "t8d76512-74f1-43bb-b1fd-87d3a8aa36df", 0, "107c13a2-fab1-4342-a601-5eeeff36bf7b", "john@skyroutes.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEGf+oZNWtdJ/ms/ZqSMfxkJtXbKRIBPUmhL7l8aTT7N6CuNbYPsD3Dn9zC3W1yqq1A==", null, false, "b1e1f147-37e1-4656-af2a-fc765f6b46ff", false, "JohnDoe" }
                });

            migrationBuilder.InsertData(
                table: "Drones",
                columns: new[] { "Id", "Callsign", "DistanceCap", "ImageLocation", "InFleetSince", "InHangar", "IsActive", "Model" },
                values: new object[,]
                {
                    { 1, "PX1", 5000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Phantom X1" },
                    { 2, "MA2", 8000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2020, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Mavic Air 2" },
                    { 3, "I2", 10000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2019, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Inspire 2" },
                    { 4, "PA", 2000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2020, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Parrot Anafi" },
                    { 5, "EVO+", 15000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2018, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Autel EVO Lite+" },
                    { 6, "Mini2", 3000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2020, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "DJI Mini 2" },
                    { 7, "Typhoon", 12000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Yuneec Typhoon H Pro" },
                    { 8, "S2", 5000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2021, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Skydio 2" },
                    { 9, "Nano", 1000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2022, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Autel EVO Nano" },
                    { 10, "Alta6", 18000, "https://www.bhphotovideo.com/cdn-cgi/image/format=auto,fit=scale-down,width=500,quality=95/https://www.bhphotovideo.com/images/images500x500/sony_airpeak_drone_1623324938_1617585.jpg", new DateTime(2019, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "FreeFly Alta 6" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Address", "Delivered", "DeliveryDate", "Distance", "IsHub", "Latitude", "Longitude", "RouteId" },
                values: new object[,]
                {
                    { 7, "548 Cedar St, Nashville, TN", false, new DateTime(2023, 11, 1, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9760), null, null, 36.044502899999998, -86.706540399999994, null },
                    { 8, "827 Redwood St, Nashville, TN", false, new DateTime(2023, 11, 1, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9760), null, null, 36.056384899999998, -86.7801446, null },
                    { 9, "615 Spruce St, Nashville, TN", false, new DateTime(2023, 11, 1, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9770), null, null, 36.159659400000002, -86.80638239999999, null },
                    { 10, "2929 Willow St, Nashville, TN", false, new DateTime(2023, 11, 1, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9790), null, null, 36.154700099999999, -86.753109100000003, null },
                    { 11, "3232 Sycamore Ln, Nashville, TN", false, new DateTime(2023, 11, 1, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9790), null, null, 36.112917699999997, -86.829324700000001, null }
                });

            migrationBuilder.InsertData(
                table: "RepairTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Propeller Replacement" },
                    { 2, "Battery Connector Repair" },
                    { 3, "Gimbal Stabilization" },
                    { 4, "Motor Replacement" },
                    { 5, "GPS Module Troubleshooting" },
                    { 6, "Camera Sensor Cleaning" },
                    { 7, "ESC Calibration" },
                    { 8, "Controller Calibration" },
                    { 9, "Frame Repair or Replacement" },
                    { 10, "Firmware Updates" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "58eeb428-a7c2-4919-adf7-5aecc885932d", "abc40bc6-0829-4ac5-a3ed-180f5e916a5f" },
                    { "205f69b3-c8dc-4e19-a813-ed3608226ced", "e224a03d-bf0c-4a05-b728-e3521e45d74d" },
                    { "205f69b3-c8dc-4e19-a813-ed3608226ced", "ece89d88-75da-4a80-9b0d-3fe58582b8e2" },
                    { "686c585a-525e-4938-a8a4-0669f463a7d7", "t7d21fac-3b21-454a-a747-075f072d0cf3" },
                    { "686c585a-525e-4938-a8a4-0669f463a7d7", "t806cfae-bda9-47c5-8473-dd52fd056a9b" },
                    { "686c585a-525e-4938-a8a4-0669f463a7d7", "t8d76512-74f1-43bb-b1fd-87d3a8aa36df" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "CreateDateTime", "FirstName", "IdentityUserId", "IsActive", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "abc40bc6-0829-4ac5-a3ed-180f5e916a5f", true, "Maverick" },
                    { 2, new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "t8d76512-74f1-43bb-b1fd-87d3a8aa36df", true, "Doe" },
                    { 3, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "t7d21fac-3b21-454a-a747-075f072d0cf3", true, "Smith" },
                    { 4, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice", "t806cfae-bda9-47c5-8473-dd52fd056a9b", true, "Johnson" },
                    { 5, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob", "ece89d88-75da-4a80-9b0d-3fe58582b8e2", true, "Williams" },
                    { 6, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eve", "e224a03d-bf0c-4a05-b728-e3521e45d74d", true, "Davis" }
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "DeliveredOn", "DroneId", "ExpeditorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 2, new DateTime(2023, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5 },
                    { 3, new DateTime(2023, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "DroneId", "InRepairSince", "Open", "OutOfRepair", "RepairSummary", "RepairTypeId", "SubmittedById", "TechnicianId" },
                values: new object[] { 1, 1, new DateTime(2023, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simple propeller blade repl", 1, 6, 4 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Address", "Delivered", "DeliveryDate", "Distance", "IsHub", "Latitude", "Longitude", "RouteId" },
                values: new object[,]
                {
                    { 1, "123 Main St, Nashville, TN", false, new DateTime(2023, 10, 26, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9720), null, null, 36.162700000000001, -86.781599999999997, 1 },
                    { 2, "456 Elm St, Nashville, TN", false, new DateTime(2023, 10, 26, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9720), null, null, 36.156300000000002, -86.781599999999997, 1 },
                    { 3, "789 Oak St, Nashville, TN", false, new DateTime(2023, 10, 26, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9740), null, null, 36.154200000000003, -86.784499999999994, 1 },
                    { 4, "1055 Pine St, Nashville, TN", true, new DateTime(2023, 10, 23, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9740), null, null, 36.152452199999999, -86.782558899999998, 2 },
                    { 5, "610 Birch Glen Ct, Nashville, TN", true, new DateTime(2023, 10, 23, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9750), null, null, 36.071896600000002, -86.969204200000007, 2 },
                    { 6, "400 Maple St, Nashville, TN", true, new DateTime(2023, 10, 23, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9750), null, null, 36.261515600000003, -86.705105099999997, 2 },
                    { 12, "3535 Juniper St, Nashville, TN", true, new DateTime(2023, 10, 22, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9790), null, null, 36.1616, -86.781000000000006, 3 },
                    { 13, "3838 Cedar Elm St, Nashville, TN", true, new DateTime(2023, 10, 22, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9800), null, null, 36.1678, -86.770399999999995, 3 },
                    { 14, "4141 Red Oak St, Nashville, TN", true, new DateTime(2023, 10, 22, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9800), null, null, 36.159700000000001, -86.784099999999995, 3 },
                    { 15, "4444 Dogwood St, Nashville, TN", true, new DateTime(2023, 10, 22, 14, 25, 4, 443, DateTimeKind.Local).AddTicks(320), null, null, 36.163400000000003, -86.777500000000003, 3 },
                    { 16, "4747 Chestnut St, Nashville, TN", true, new DateTime(2023, 10, 22, 14, 25, 4, 443, DateTimeKind.Local).AddTicks(330), null, null, 36.156799999999997, -86.775300000000001, 3 },
                    { 17, "5050 Willow Oak St, Nashville, TN", true, new DateTime(2023, 10, 22, 14, 25, 4, 443, DateTimeKind.Local).AddTicks(330), null, null, 36.167099999999998, -86.781199999999998, 3 },
                    { 123, "50 Airways Blvd, Nashville, TN", false, new DateTime(2023, 10, 26, 14, 25, 4, 442, DateTimeKind.Local).AddTicks(9620), null, true, 36.130043100000002, -86.691946099999996, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RouteId",
                table: "Orders",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_DroneId",
                table: "Routes",
                column: "DroneId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ExpeditorId",
                table: "Routes",
                column: "ExpeditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DroneId",
                table: "Tickets",
                column: "DroneId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RepairTypeId",
                table: "Tickets",
                column: "RepairTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TechnicianId",
                table: "Tickets",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_IdentityUserId",
                table: "UserProfiles",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "RepairTypes");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
