using Microsoft.EntityFrameworkCore.Migrations;

namespace FORO_C.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Habilitado",
                table: "MiembrosEntradas",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Habilitado",
                table: "MiembrosEntradas",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
