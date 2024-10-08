using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanetSurveillance.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HairColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkinColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EyeColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Homeworld = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vehicles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Starships = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edited = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SWAPIId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotationPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrbitalPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Climate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gravity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Terrain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurfaceWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Population = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Residents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edited = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SWAPIId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PlanetId = table.Column<int>(type: "int", nullable: false),
                    DateOfVisit = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PersonId",
                table: "Visits",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PlanetId",
                table: "Visits",
                column: "PlanetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Planets");
        }
    }
}
