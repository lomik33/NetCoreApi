using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreApi.Migrations
{
    public partial class mig02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnidadesMedida",
                columns: table => new
                {
                    UnidadDeMedidaId = table.Column<Guid>(nullable: false),
                    NombreUnidad = table.Column<string>(maxLength: 150, nullable: false),
                    AbreviacionUnidad = table.Column<string>(maxLength: 10, nullable: false),
                    TipoUnidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMedida", x => x.UnidadDeMedidaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnidadesMedida");
        }
    }
}
