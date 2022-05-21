using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animals.Api.Migrations
{
    public partial class AnimalUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOfBirth",
                table: "Specie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnimalHistoryAge",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProtectedSpecie",
                table: "Animals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Left",
                table: "Animals",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfBirth",
                table: "Specie");

            migrationBuilder.DropColumn(
                name: "AnimalHistoryAge",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "IsProtectedSpecie",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Left",
                table: "Animals");
        }
    }
}
