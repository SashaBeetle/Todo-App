using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncludeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardsId",
                table: "Catalogs");

            migrationBuilder.DropColumn(
                name: "CatalogsId",
                table: "Boards");

            migrationBuilder.AddColumn<int>(
                name: "CatalogId",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CatalogId",
                table: "Cards",
                column: "CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Catalogs_CatalogId",
                table: "Cards",
                column: "CatalogId",
                principalTable: "Catalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<List<int>>(
                name: "CardsId",
                table: "Catalogs",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddColumn<List<int>>(
                name: "CatalogsId",
                table: "Boards",
                type: "integer[]",
                nullable: false);
        }
    }
}
