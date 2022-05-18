using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animals.Api.Migrations
{
    public partial class FixingHomeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Specie_SpecieId",
                table: "Animals");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Specie_SpecieId",
                table: "Animals",
                column: "SpecieId",
                principalTable: "Specie",
                principalColumn: "SpecieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Specie_SpecieId",
                table: "Animals");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Specie_SpecieId",
                table: "Animals",
                column: "SpecieId",
                principalTable: "Specie",
                principalColumn: "SpecieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
