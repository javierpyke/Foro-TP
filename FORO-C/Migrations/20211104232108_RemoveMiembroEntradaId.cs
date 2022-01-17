using Microsoft.EntityFrameworkCore.Migrations;

namespace FORO_C.Migrations
{
    public partial class RemoveMiembroEntradaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntradaId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<bool>(
                name: "Habilitado",
                table: "MiembrosEntradas",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntradaId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Habilitado",
                table: "MiembrosEntradas",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
