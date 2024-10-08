using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanetSurveillance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonAndPlanetIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Visits",
                newName: "VisitId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Planets",
                newName: "PlanetId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Persons",
                newName: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisitId",
                table: "Visits",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlanetId",
                table: "Planets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Persons",
                newName: "Id");
        }
    }
}
