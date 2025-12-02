using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportSystemTyp = table.Column<int>(type: "int", nullable: false),
                    BrytDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    ConnectionStringWinLas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OmraknadWinLas = table.Column<DateOnly>(type: "date", nullable: false),
                    AnvandVarjeArbetsDagSomEnPerionVidSava = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Personnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HistorisktBrytDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    HistoriskTid = table.Column<int>(type: "int", nullable: false),
                    TillagsTid = table.Column<int>(type: "int", nullable: false),
                    AnstallningsTid = table.Column<int>(type: "int", nullable: false),
                    ForetradeAllmanIdagTid = table.Column<int>(type: "int", nullable: false),
                    ForetradeSavIdagTid = table.Column<int>(type: "int", nullable: false),
                    KonverteringSavIdagTid = table.Column<int>(type: "int", nullable: false),
                    KonverteringVikIdagTid = table.Column<int>(type: "int", nullable: false),
                    KonverteringAvaIdagTid = table.Column<int>(type: "int", nullable: false),
                    SenatsteAnstallning = table.Column<DateOnly>(type: "date", nullable: false),
                    ForetradeAllmanDatum = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personer_Kunder_KundId",
                        column: x => x.KundId,
                        principalTable: "Kunder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anstallningar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<DateOnly>(type: "date", nullable: false),
                    Tom = table.Column<DateOnly>(type: "date", nullable: true),
                    Anstallningsnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvtalKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnstallningsKlassificeringTyp = table.Column<int>(type: "int", nullable: false),
                    AnstallningsKlassificeringKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvgangsorsaksTyp = table.Column<int>(type: "int", nullable: false),
                    AvgangsorsakKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganisationsKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganisationsText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BefattningsKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BefattninsText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BefattningsGruppering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sysselsattningsgrad = table.Column<float>(type: "real", nullable: false),
                    LoneformTyp = table.Column<int>(type: "int", nullable: false),
                    Last = table.Column<bool>(type: "bit", nullable: false),
                    Vilande = table.Column<bool>(type: "bit", nullable: false),
                    Regtid = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anstallningar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anstallningar_Personer_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Personer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WinLasData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WinLasPersonid = table.Column<int>(type: "int", nullable: false),
                    Senaste = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalAnstallningsTid = table.Column<int>(type: "int", nullable: false),
                    KonverteringVikIdagTid = table.Column<int>(type: "int", nullable: false),
                    KonverteringSavIdagTid = table.Column<int>(type: "int", nullable: false),
                    KonverteringVikSenasteTid = table.Column<int>(type: "int", nullable: false),
                    KonverteringSavSenasteTid = table.Column<int>(type: "int", nullable: false),
                    ForetradeAllmanIdagTid = table.Column<int>(type: "int", nullable: false),
                    ForetradeAllmanSenasteTid = table.Column<int>(type: "int", nullable: false),
                    ForetradeSavSenasteTid = table.Column<int>(type: "int", nullable: false),
                    ForetradeSavIdagTid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinLasData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WinLasData_Personer_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Personer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anstallningar_PersonId",
                table: "Anstallningar",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Personer_KundId",
                table: "Personer",
                column: "KundId");

            migrationBuilder.CreateIndex(
                name: "IX_WinLasData_PersonId",
                table: "WinLasData",
                column: "PersonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anstallningar");

            migrationBuilder.DropTable(
                name: "WinLasData");

            migrationBuilder.DropTable(
                name: "Personer");

            migrationBuilder.DropTable(
                name: "Kunder");
        }
    }
}
