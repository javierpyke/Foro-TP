using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FORO_C.Migrations
{
    public partial class MiembrosEntradasNullHabilitado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Habilitado",
                table: "MiembrosEntradas",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "Entradas",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Habilitado",
                table: "MiembrosEntradas",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "Entradas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
