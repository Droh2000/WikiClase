using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiClase.Migrations
{
    public partial class TagFuncion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Posts_PostId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PostId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Tags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Tags",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostId",
                table: "Tags",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Posts_PostId",
                table: "Tags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
