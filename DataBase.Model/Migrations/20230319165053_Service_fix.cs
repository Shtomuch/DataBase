using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Model.Migrations
{
    /// <inheritdoc />
    public partial class Service_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_EntertainmentCenters_EntertainmentCentersId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "EntertainmentCentersId",
                table: "Service",
                newName: "EntertainmentCenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_EntertainmentCentersId",
                table: "Service",
                newName: "IX_Service_EntertainmentCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_EntertainmentCenters_EntertainmentCenterId",
                table: "Service",
                column: "EntertainmentCenterId",
                principalTable: "EntertainmentCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_EntertainmentCenters_EntertainmentCenterId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "EntertainmentCenterId",
                table: "Service",
                newName: "EntertainmentCentersId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_EntertainmentCenterId",
                table: "Service",
                newName: "IX_Service_EntertainmentCentersId");

            migrationBuilder.AddColumn<float>(
                name: "Cost",
                table: "Service",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_EntertainmentCenters_EntertainmentCentersId",
                table: "Service",
                column: "EntertainmentCentersId",
                principalTable: "EntertainmentCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
