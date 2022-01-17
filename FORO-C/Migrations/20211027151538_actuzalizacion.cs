using Microsoft.EntityFrameworkCore.Migrations;

namespace FORO_C.Migrations
{
    public partial class actuzalizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreguntaId",
                table: "Entradas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreguntaId",
                table: "Entradas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
