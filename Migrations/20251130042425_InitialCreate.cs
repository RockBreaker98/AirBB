using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SSN = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    DOB = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Accommodation = table.Column<int>(type: "INTEGER", nullable: false),
                    Bedrooms = table.Column<int>(type: "INTEGER", nullable: false),
                    Bathrooms = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    BuiltYear = table.Column<int>(type: "INTEGER", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ResidencePicture = table.Column<string>(type: "TEXT", nullable: true),
                    GuestNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BedroomNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BathroomNumber = table.Column<decimal>(type: "TEXT", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.ResidenceId);
                    table.ForeignKey(
                        name: "FK_Residences_Clients_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Residences_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservationStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReservationEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Residences_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residences",
                        principalColumn: "ResidenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "DOB", "Email", "Name", "PhoneNumber", "SSN", "UserType" },
                values: new object[] { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "owner@example.com", "John Owner", "1111111111", "000000000", 1 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "City", "Country", "Name", "State" },
                values: new object[,]
                {
                    { 1, "Boston", null, "Boston", null },
                    { 2, "Chicago", null, "Chicago", null },
                    { 3, "New York", null, "New York", null }
                });

            migrationBuilder.InsertData(
                table: "Residences",
                columns: new[] { "ResidenceId", "Accommodation", "BathroomNumber", "Bathrooms", "BedroomNumber", "Bedrooms", "BuiltYear", "GuestNumber", "Image", "LocationId", "Name", "OwnerId", "Price", "PricePerNight", "ResidencePicture" },
                values: new object[,]
                {
                    { 101, 4, 1.0m, 1.0m, 2, 2, 2005, 4, "boston.jpg", 1, "Boston Back Bay Condo", 1, 179m, 179m, "boston.jpg" },
                    { 102, 3, 1.0m, 1.0m, 1, 1, 2010, 3, "chicago.jpg", 2, "Chicago Loop Apartment", 1, 129m, 129m, "chicago.jpg" },
                    { 103, 2, 1.0m, 1.0m, 1, 1, 2015, 2, "nyc.jpg", 3, "NYC Midtown Studio", 1, 149m, 149m, "nyc.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CategoryId",
                table: "Experiences",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ResidenceId",
                table: "Reservations",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_LocationId",
                table: "Residences",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_OwnerId",
                table: "Residences",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Residences");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
