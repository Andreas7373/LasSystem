using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KonverteringAvaIdagTid",
                table: "Personer");

            migrationBuilder.RenameColumn(
                name: "SenatsteAnstallning",
                table: "Personer",
                newName: "ForetradeSavDatum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForetradeSavDatum",
                table: "Personer",
                newName: "SenatsteAnstallning");

            migrationBuilder.AddColumn<int>(
                name: "KonverteringAvaIdagTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
