using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace todo_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatalogId",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Cards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Cards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CardsId = table.Column<List<int>>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Catalogs_CatalogId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CatalogId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CatalogId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Cards");
        }
    }
}
