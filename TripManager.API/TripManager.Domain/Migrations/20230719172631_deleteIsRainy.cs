using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripManager.Domain.Migrations
{
    /// <inheritdoc />
    public partial class deleteIsRainy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRainy",
                table: "Trip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRainy",
                table: "Trip",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
