using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpelKodeordsmanager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Username : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Managers");
        }
    }
}
