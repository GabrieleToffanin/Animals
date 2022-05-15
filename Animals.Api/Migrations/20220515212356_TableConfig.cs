using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animals.Api.Migrations
{
    public partial class TableConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Species_Specie_Id",
                table: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Species",
                table: "Species");

            migrationBuilder.RenameTable(
                name: "Species",
                newName: "Specie");

            migrationBuilder.AlterColumn<int>(
                name: "Specie_Id",
                table: "Animals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Specie",
                table: "Animals",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specie",
                table: "Specie",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specie",
                table: "Specie");

            migrationBuilder.DropColumn(
                name: "Specie",
                table: "Animals");

            migrationBuilder.RenameTable(
                name: "Specie",
                newName: "Species");

            migrationBuilder.AlterColumn<int>(
                name: "Specie_Id",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Species",
                table: "Species",
                column: "Specie_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Species_Specie_Id",
                table: "Animals",
                column: "Specie_Id",
                principalTable: "Species",
                principalColumn: "Specie_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
