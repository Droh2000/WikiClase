using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiClase.Migrations
{
    public partial class quinta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Contenido",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contenido",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
