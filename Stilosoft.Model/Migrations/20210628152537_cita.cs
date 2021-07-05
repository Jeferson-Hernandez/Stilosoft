using Microsoft.EntityFrameworkCore.Migrations;

namespace Stilosoft.Model.Migrations
{
    public partial class cita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Cliente_ClienteId1",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCita_Estilista_EstilistaId",
                table: "DetalleCita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_ClienteId1",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Cita");

            migrationBuilder.AlterColumn<int>(
                name: "EstilistaId",
                table: "DetalleCita",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ClienteId",
                table: "Cita",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ClienteId",
                table: "Cita",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Cliente_ClienteId",
                table: "Cita",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCita_Estilista_EstilistaId",
                table: "DetalleCita",
                column: "EstilistaId",
                principalTable: "Estilista",
                principalColumn: "EstilistaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Cliente_ClienteId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCita_Estilista_EstilistaId",
                table: "DetalleCita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_ClienteId",
                table: "Cita");

            migrationBuilder.AlterColumn<int>(
                name: "EstilistaId",
                table: "DetalleCita",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Cita",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteId1",
                table: "Cita",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ClienteId1",
                table: "Cita",
                column: "ClienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Cliente_ClienteId1",
                table: "Cita",
                column: "ClienteId1",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCita_Estilista_EstilistaId",
                table: "DetalleCita",
                column: "EstilistaId",
                principalTable: "Estilista",
                principalColumn: "EstilistaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
