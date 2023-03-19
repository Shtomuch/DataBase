using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Model.Migrations
{
    /// <inheritdoc />
    public partial class ElectroCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParkingCount",
                table: "EntertainmentCenters",
                newName: "ElectroCharge");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ElectroCharge",
                table: "EntertainmentCenters",
                newName: "ParkingCount");
        }
    }
}
