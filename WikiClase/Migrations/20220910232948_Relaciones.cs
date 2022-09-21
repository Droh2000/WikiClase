using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiClase.Migrations
{
    public partial class Relaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "SubCategoria",
                table: "Tags",
                newName: "subCategoria");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoriaId",
                table: "Posts",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TagId",
                table: "Posts",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categorias_CategoriaId",
                table: "Posts",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Tags_TagId",
                table: "Posts",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categorias_CategoriaId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Tags_TagId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CategoriaId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TagId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "subCategoria",
                table: "Tags",
                newName: "SubCategoria");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Tags",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
