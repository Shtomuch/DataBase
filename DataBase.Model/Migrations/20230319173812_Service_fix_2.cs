using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Model.Migrations
{
    /// <inheritdoc />
    public partial class Service_fix_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriptions",
                table: "Service",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Service",
                newName: "Descriptions");
        }
    }
}
