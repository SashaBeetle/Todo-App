using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoryRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HistoryItems_BoardId",
                table: "HistoryItems",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryItems_CardId",
                table: "HistoryItems",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryItems_Boards_BoardId",
                table: "HistoryItems",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryItems_Cards_CardId",
                table: "HistoryItems",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryItems_Boards_BoardId",
                table: "HistoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryItems_Cards_CardId",
                table: "HistoryItems");

            migrationBuilder.DropIndex(
                name: "IX_HistoryItems_BoardId",
                table: "HistoryItems");

            migrationBuilder.DropIndex(
                name: "IX_HistoryItems_CardId",
                table: "HistoryItems");
        }
    }
}
