using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magix_api.Migrations
{
    /// <inheritdoc />
    public partial class FinishedGameModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Factions_FactionId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckCards_Cards_CardId",
                table: "DeckCards");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckCards_Cards_CardIdRef",
                table: "DeckCards");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckCards_Decks_DeckIdRef",
                table: "DeckCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_playerId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeckCards",
                table: "DeckCards");

            migrationBuilder.DropIndex(
                name: "IX_DeckCards_CardIdRef",
                table: "DeckCards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_FactionId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "DeckIdRef",
                table: "DeckCards");

            migrationBuilder.DropColumn(
                name: "FactionId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "playerId",
                table: "Games",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_playerId",
                table: "Games",
                newName: "IX_Games_PlayerId");

            migrationBuilder.RenameColumn(
                name: "CardIdRef",
                table: "DeckCards",
                newName: "DeckId");

            migrationBuilder.AlterColumn<string>(
                name: "Opponent",
                table: "Games",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "DeckCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId1",
                table: "DeckCards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardName",
                table: "Cards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "FactionName",
                table: "Cards",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeckCards",
                table: "DeckCards",
                columns: new[] { "DeckId", "CardId" });

            migrationBuilder.CreateIndex(
                name: "IX_Players_Username",
                table: "Players",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeckCards_CardId1",
                table: "DeckCards",
                column: "CardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCards_Cards_CardId",
                table: "DeckCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCards_Cards_CardId1",
                table: "DeckCards",
                column: "CardId1",
                principalTable: "Cards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCards_Decks_DeckId",
                table: "DeckCards",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerId",
                table: "Games",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeckCards_Cards_CardId",
                table: "DeckCards");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckCards_Cards_CardId1",
                table: "DeckCards");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckCards_Decks_DeckId",
                table: "DeckCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Players_Username",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeckCards",
                table: "DeckCards");

            migrationBuilder.DropIndex(
                name: "IX_DeckCards_CardId1",
                table: "DeckCards");

            migrationBuilder.DropColumn(
                name: "CardId1",
                table: "DeckCards");

            migrationBuilder.DropColumn(
                name: "FactionName",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Games",
                newName: "playerId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_PlayerId",
                table: "Games",
                newName: "IX_Games_playerId");

            migrationBuilder.RenameColumn(
                name: "DeckId",
                table: "DeckCards",
                newName: "CardIdRef");

            migrationBuilder.AlterColumn<string>(
                name: "Opponent",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "DeckCards",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "DeckIdRef",
                table: "DeckCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CardName",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactionId",
                table: "Cards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeckCards",
                table: "DeckCards",
                columns: new[] { "DeckIdRef", "CardIdRef" });

            migrationBuilder.CreateIndex(
                name: "IX_DeckCards_CardIdRef",
                table: "DeckCards",
                column: "CardIdRef");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_FactionId",
                table: "Cards",
                column: "FactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Factions_FactionId",
                table: "Cards",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCards_Cards_CardId",
                table: "DeckCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCards_Cards_CardIdRef",
                table: "DeckCards",
                column: "CardIdRef",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCards_Decks_DeckIdRef",
                table: "DeckCards",
                column: "DeckIdRef",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_playerId",
                table: "Games",
                column: "playerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
