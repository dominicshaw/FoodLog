using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodLog.Api.Migrations
{
    public partial class initial_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    EntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    Breakfast = table.Column<string>(maxLength: 200, nullable: true),
                    Lunch = table.Column<string>(nullable: true),
                    Dinner = table.Column<string>(nullable: true),
                    SnacksDrinks = table.Column<string>(nullable: true),
                    Dairy = table.Column<bool>(nullable: false),
                    Gluten = table.Column<bool>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Alcohol = table.Column<bool>(nullable: false),
                    Caffeine = table.Column<bool>(nullable: false),
                    FattyFood = table.Column<bool>(nullable: false),
                    Spice = table.Column<bool>(nullable: false),
                    OnionsPulses = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.EntryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");
        }
    }
}
