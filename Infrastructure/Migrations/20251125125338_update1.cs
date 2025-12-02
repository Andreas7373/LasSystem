using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForetradeAllmanDagar",
                table: "Kunder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ForetradeSavDagar",
                table: "Kunder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KonverteringLASDagar",
                table: "Kunder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KonverteringSavDagar",
                table: "Kunder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KonverteringVikDagar",
                table: "Kunder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pensionsalder",
                table: "Kunder",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForetradeAllmanDagar",
                table: "Kunder");

            migrationBuilder.DropColumn(
                name: "ForetradeSavDagar",
                table: "Kunder");

            migrationBuilder.DropColumn(
                name: "KonverteringLASDagar",
                table: "Kunder");

            migrationBuilder.DropColumn(
                name: "KonverteringSavDagar",
                table: "Kunder");

            migrationBuilder.DropColumn(
                name: "KonverteringVikDagar",
                table: "Kunder");

            migrationBuilder.DropColumn(
                name: "Pensionsalder",
                table: "Kunder");
        }
    }
}
