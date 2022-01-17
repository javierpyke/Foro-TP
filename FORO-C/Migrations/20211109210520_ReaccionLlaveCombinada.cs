using Microsoft.EntityFrameworkCore.Migrations;

namespace FORO_C.Migrations
{
    public partial class ReaccionLlaveCombinada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reacciones",
                table: "Reacciones");

            migrationBuilder.DropIndex(
                name: "IX_Reacciones_MiembroId",
                table: "Reacciones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reacciones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reacciones",
                table: "Reacciones",
                columns: new[] { "MiembroId", "RespuestaId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reacciones",
                table: "Reacciones");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reacciones",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reacciones",
                table: "Reacciones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reacciones_MiembroId",
                table: "Reacciones",
                column: "MiembroId");
        }
    }
}
