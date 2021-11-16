using Microsoft.EntityFrameworkCore.Migrations;

namespace Stilosoft.Model.Migrations
{
    public partial class Abono : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MontoAbonandoTotal",
                table: "AbonoCompra",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoAbonandoTotal",
                table: "AbonoCompra");
        }
    }
}
