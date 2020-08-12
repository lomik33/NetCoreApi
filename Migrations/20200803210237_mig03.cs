using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreApi.Migrations
{
    public partial class mig03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comida",
                columns: table => new
                {
                    ComidaId = table.Column<Guid>(nullable: false),
                    NombreComida = table.Column<string>(nullable: false),
                    TipoComida = table.Column<int>(nullable: false),
                    PrecioComida = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DescripcionComida = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comida", x => x.ComidaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comida");
        }
    }
}
