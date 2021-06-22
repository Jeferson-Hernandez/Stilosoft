using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stilosoft.Model.Migrations
{
    public partial class estilista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estilista",
                columns: table => new
                {
                    EstilistaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "Date", nullable: false),
                    cedula = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    celular = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estilista", x => x.EstilistaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estilista");
        }
    }
}
