using Microsoft.EntityFrameworkCore.Migrations;

namespace Stilosoft.Model.Migrations
{
    public partial class detallecompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantInsumo",
                table: "DetalleCompra");

            migrationBuilder.DropColumn(
                name: "CantProducto",
                table: "DetalleCompra");

            migrationBuilder.DropColumn(
                name: "InsumoId",
                table: "DetalleCompra");

            migrationBuilder.DropColumn(
                name: "Medida",
                table: "DetalleCompra");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantInsumo",
                table: "DetalleCompra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantProducto",
                table: "DetalleCompra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsumoId",
                table: "DetalleCompra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Medida",
                table: "DetalleCompra",
                type: "nvarchar(20)",
                nullable: true);
        }
    }
}
