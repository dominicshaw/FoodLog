using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodLog.Api.Migrations
{
    public partial class exercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Exercise",
                table: "Entries",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exercise",
                table: "Entries");
        }
    }
}
