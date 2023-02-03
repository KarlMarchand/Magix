using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace magixapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    @class = table.Column<string>(name: "class", type: "character varying(32)", maxLength: 32, nullable: true),
                    trophies = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
                    besttrophyscore = table.Column<int>(name: "best_trophy_score", type: "integer", nullable: true, defaultValueSql: "0"),
                    faction = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true, defaultValueSql: "'rebel'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("player_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    player = table.Column<int>(type: "integer", nullable: false),
                    @class = table.Column<string>(name: "class", type: "character varying(32)", maxLength: 32, nullable: true),
                    opponent = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    won = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("game_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_game_player",
                        column: x => x.player,
                        principalTable: "player",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "played_cards",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    player = table.Column<int>(type: "integer", nullable: false),
                    cardid = table.Column<int>(name: "card_id", type: "integer", nullable: false),
                    timeplayed = table.Column<int>(name: "time_played", type: "integer", nullable: false, defaultValueSql: "1"),
                    victory = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("played_cards_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_playc_game",
                        column: x => x.player,
                        principalTable: "player",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_player",
                table: "game",
                column: "player");

            migrationBuilder.CreateIndex(
                name: "IX_played_cards_player",
                table: "played_cards",
                column: "player");

            migrationBuilder.CreateIndex(
                name: "player_username_key",
                table: "player",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game");

            migrationBuilder.DropTable(
                name: "played_cards");

            migrationBuilder.DropTable(
                name: "player");
        }
    }
}
