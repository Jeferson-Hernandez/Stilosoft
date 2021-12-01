using Microsoft.EntityFrameworkCore.Migrations;

namespace Stilosoft.Model.Migrations
{
    public partial class solicitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudServicio_Cliente_ClienteId1",
                table: "SolicitudServicio");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudServicio_ClienteId1",
                table: "SolicitudServicio");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "SolicitudServicio");

            migrationBuilder.AlterColumn<string>(
                name: "ClienteId",
                table: "SolicitudServicio",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudServicio_ClienteId",
                table: "SolicitudServicio",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudServicio_Cliente_ClienteId",
                table: "SolicitudServicio",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudServicio_Cliente_ClienteId",
                table: "SolicitudServicio");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudServicio_ClienteId",
                table: "SolicitudServicio");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "SolicitudServicio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteId1",
                table: "SolicitudServicio",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudServicio_ClienteId1",
                table: "SolicitudServicio",
                column: "ClienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudServicio_Cliente_ClienteId1",
                table: "SolicitudServicio",
                column: "ClienteId1",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
