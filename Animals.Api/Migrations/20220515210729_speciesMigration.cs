using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animals.Api.Migrations
{
    public partial class speciesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Specie_Id",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Specie_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecieName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Specie_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_Specie_Id",
                table: "Animals",
                column: "Specie_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Species_Specie_Id",
                table: "Animals",
                column: "Specie_Id",
                principalTable: "Species",
                principalColumn: "Specie_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Species_Specie_Id",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Animals_Specie_Id",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Specie_Id",
                table: "Animals");

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Tiger" });
        }
    }
}
