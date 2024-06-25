using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityIntoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Catalogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_BoardId",
                table: "Catalogs",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalogs_Boards_BoardId",
                table: "Catalogs",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalogs_Boards_BoardId",
                table: "Catalogs");

            migrationBuilder.DropIndex(
                name: "IX_Catalogs_BoardId",
                table: "Catalogs");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Catalogs");
        }
    }
}
