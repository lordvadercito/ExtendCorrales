using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExtendCorrales.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtendCorral",
                columns: table => new
                {
                    ExtendCorralId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    TropaId = table.Column<int>(nullable: false),
                    NroTropa = table.Column<int>(nullable: false),
                    FechaLlegada = table.Column<DateTime>(nullable: false),
                    HoraLlegada = table.Column<string>(nullable: true),
                    Corral = table.Column<int>(nullable: false),
                    Raza = table.Column<string>(nullable: true),
                    Categoria = table.Column<string>(nullable: true),
                    Edad = table.Column<string>(nullable: true),
                    PesoGeneral = table.Column<string>(nullable: true),
                    EstadoGeneral = table.Column<string>(nullable: true),
                    Condicion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendCorral", x => x.ExtendCorralId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtendCorral");
        }
    }
}
