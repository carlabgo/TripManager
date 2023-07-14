using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TripManager.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AgregoVehicleTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VehicleType",
                columns: new[] { "Id", "Active", "Name" },
                values: new object[,]
                {
                    { 1L, false, "Auto" },
                    { 2L, false, "Camion" },
                    { 3L, false, "Moto" },
                    { 4L, false, "Motorhome" },
                    { 5L, false, "Camioneta" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleType");
        }
    }
}
