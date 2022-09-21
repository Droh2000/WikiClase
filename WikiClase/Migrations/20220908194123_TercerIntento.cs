using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiClase.Migrations
{
    public partial class TercerIntento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "nombreImagen",
                table: "Posts",
                newName: "Autor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "Posts",
                newName: "nombreImagen");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Posts",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
