using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripManager.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Vehicle");

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Vehicle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_TypeId",
                table: "Vehicle",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleType_TypeId",
                table: "Vehicle",
                column: "TypeId",
                principalTable: "VehicleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleType_TypeId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_TypeId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Vehicle");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
