using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upgradeDatum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ForetradeAllmanSenasteAnstallningDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "ForetradeAllmanSenasteTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ForetradeSasongDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "ForetradeSasongIdagTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ForetradeSasongSenasteAnstallningDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "ForetradeSasongSenasteTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ForetradeSavSenasteAnstallningDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "ForetradeSavSenasteTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "KonverteringAvaDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "KonverteringAvaIdagTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "KonverteringAvaSenasteAnstallningDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "KonverteringAvaSenasteTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "KonverteringSavDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "KonverteringSavSenasteAnstallningDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "KonverteringSavSenasteTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "KonverteringVikDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "KonverteringVikSenasteAnstallningDatum",
                table: "Personer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "KonverteringVikSenasteTid",
                table: "Personer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForetradeAllmanSenasteAnstallningDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "ForetradeAllmanSenasteTid",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "ForetradeSasongDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "ForetradeSasongIdagTid",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "ForetradeSasongSenasteAnstallningDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "ForetradeSasongSenasteTid",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "ForetradeSavSenasteAnstallningDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "ForetradeSavSenasteTid",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringAvaDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringAvaIdagTid",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringAvaSenasteAnstallningDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringAvaSenasteTid",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringSavDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringSavSenasteAnstallningDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringSavSenasteTid",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringVikDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringVikSenasteAnstallningDatum",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "KonverteringVikSenasteTid",
                table: "Personer");
        }
    }
}
