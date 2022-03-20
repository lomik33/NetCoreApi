using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreApi.Migrations
{
    public partial class mig04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    EmpleadoId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 250, nullable: false),
                    ApellidoPaterno = table.Column<string>(maxLength: 150, nullable: false),
                    ApellidoMaterno = table.Column<string>(maxLength: 150, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Genero = table.Column<int>(maxLength: 30, nullable: false),
                    Rfc = table.Column<string>(maxLength: 20, nullable: false),
                    Nss = table.Column<string>(maxLength: 20, nullable: false),
                    EstadoCivil = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.EmpleadoId);
                });

            migrationBuilder.CreateTable(
                name: "InformacionContrato",
                columns: table => new
                {
                    InformacionContratoId = table.Column<Guid>(nullable: false),
                    Puesto = table.Column<string>(maxLength: 250, nullable: false),
                    PuestoDescripcion = table.Column<string>(maxLength: 5000, nullable: true),
                    TipoContrato = table.Column<int>(nullable: false),
                    FechaContratacion = table.Column<DateTime>(nullable: false),
                    FechaTermino = table.Column<DateTime>(nullable: true),
                    Salario = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionContrato", x => x.InformacionContratoId);
                    table.ForeignKey(
                        name: "FK_InformacionContrato_Empleado_InformacionContratoId",
                        column: x => x.InformacionContratoId,
                        principalTable: "Empleado",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformacionContrato");

            migrationBuilder.DropTable(
                name: "Empleado");
        }
    }
}
