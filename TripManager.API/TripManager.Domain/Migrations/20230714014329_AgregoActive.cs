using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripManager.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AgregoActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Vehicle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Trip",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "City",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "City");
        }
    }
}
