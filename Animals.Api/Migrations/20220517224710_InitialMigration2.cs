using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animals.Api.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Specie_Specie",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_Specie",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Specie",
                table: "Specie");

            migrationBuilder.DropColumn(
                name: "Specie",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "Specie_Id",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_Specie_Id",
                table: "Animals",
                column: "Specie_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Specie_Specie_Id",
                table: "Animals",
                column: "Specie_Id",
                principalTable: "Specie",
                principalColumn: "Specie_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Specie_Specie_Id",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_Specie_Id",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Specie_Id",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "Specie",
                table: "Specie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Specie",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_Specie",
                table: "Animals",
                column: "Specie");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Specie_Specie",
                table: "Animals",
                column: "Specie",
                principalTable: "Specie",
                principalColumn: "Specie_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
