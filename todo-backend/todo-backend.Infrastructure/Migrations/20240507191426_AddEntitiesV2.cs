using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitiesV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Catalogs_CatalogId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CatalogId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CatalogId",
                table: "Cards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatalogId",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CatalogId",
                table: "Cards",
                column: "CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Catalogs_CatalogId",
                table: "Cards",
                column: "CatalogId",
                principalTable: "Catalogs",
                principalColumn: "Id");
        }
    }
}
