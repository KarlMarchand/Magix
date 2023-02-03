﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using magix_api.Data;

#nullable disable

namespace magixapi.Migrations
{
    [DbContext(typeof(MagixContext))]
    partial class MagixContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("magix_api.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Class")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("class");

                    b.Property<DateOnly>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("date")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<string>("Opponent")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("opponent");

                    b.Property<int>("Player")
                        .HasColumnType("integer")
                        .HasColumnName("player");

                    b.Property<bool>("Won")
                        .HasColumnType("boolean")
                        .HasColumnName("won");

                    b.HasKey("Id")
                        .HasName("game_pkey");

                    b.HasIndex("Player");

                    b.ToTable("game", (string)null);
                });

            modelBuilder.Entity("magix_api.PlayedCard", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("integer")
                        .HasColumnName("card_id");

                    b.Property<int>("Player")
                        .HasColumnType("integer")
                        .HasColumnName("player");

                    b.Property<int>("TimePlayed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("time_played")
                        .HasDefaultValueSql("1");

                    b.Property<int>("Victory")
                        .HasColumnType("integer")
                        .HasColumnName("victory");

                    b.HasKey("Id")
                        .HasName("played_cards_pkey");

                    b.HasIndex("Player");

                    b.ToTable("played_cards", (string)null);
                });

            modelBuilder.Entity("magix_api.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BestTrophyScore")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("best_trophy_score")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Class")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("class");

                    b.Property<string>("Faction")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("faction")
                        .HasDefaultValueSql("'rebel'::character varying");

                    b.Property<int?>("Trophies")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("trophies")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("player_pkey");

                    b.HasIndex(new[] { "Username" }, "player_username_key")
                        .IsUnique();

                    b.ToTable("player", (string)null);
                });

            modelBuilder.Entity("magix_api.PlayerStat", b =>
                {
                    b.Property<int?>("BestTrophyScore")
                        .HasColumnType("integer")
                        .HasColumnName("best_trophy_score");

                    b.Property<string>("Class")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("class");

                    b.Property<string>("Faction")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("faction");

                    b.Property<long?>("GamePlayed")
                        .HasColumnType("bigint")
                        .HasColumnName("game_played");

                    b.Property<int?>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<decimal?>("Loses")
                        .HasColumnType("numeric")
                        .HasColumnName("loses");

                    b.Property<decimal?>("RatioWins")
                        .HasColumnType("numeric")
                        .HasColumnName("ratio_wins");

                    b.Property<string>("TopCards")
                        .HasColumnType("text")
                        .HasColumnName("top_cards");

                    b.Property<int?>("Trophies")
                        .HasColumnType("integer")
                        .HasColumnName("trophies");

                    b.Property<string>("Username")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("username");

                    b.Property<decimal?>("Wins")
                        .HasColumnType("numeric")
                        .HasColumnName("wins");

                    b.ToTable((string)null);

                    b.ToView("player_stats", (string)null);
                });

            modelBuilder.Entity("magix_api.Game", b =>
                {
                    b.HasOne("magix_api.Player", "PlayerNavigation")
                        .WithMany("Games")
                        .HasForeignKey("Player")
                        .IsRequired()
                        .HasConstraintName("fk_game_player");

                    b.Navigation("PlayerNavigation");
                });

            modelBuilder.Entity("magix_api.PlayedCard", b =>
                {
                    b.HasOne("magix_api.Player", "PlayerNavigation")
                        .WithMany("PlayedCards")
                        .HasForeignKey("Player")
                        .IsRequired()
                        .HasConstraintName("fk_playc_game");

                    b.Navigation("PlayerNavigation");
                });

            modelBuilder.Entity("magix_api.Player", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("PlayedCards");
                });
#pragma warning restore 612, 618
        }
    }
}
