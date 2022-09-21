using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiClase.Migrations
{
    public partial class ImagenPrueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Posts",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Posts");
        }
    }
}
