using Microsoft.EntityFrameworkCore;

namespace magix_api.Data
{
    public partial class MagixContext : DbContext
    {
        public MagixContext()
        {
        }

        public MagixContext(DbContextOptions<MagixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Deck> Decks { get; set; }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<PlayedCard> PlayedCards { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<PlayerStat> PlayerStats { get; set; }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Faction> Factions { get; set; }
        public virtual DbSet<Hero> Heroes { get; set; }
        public virtual DbSet<Talent> Talents { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deck>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("deck_pkey");

                entity.ToTable("deck");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("true")
                    .HasColumnName("active");
                entity.Property(e => e.Cards).HasColumnName("cards");
                entity.Property(e => e.Class)
                    .HasMaxLength(32)
                    .HasColumnName("class");
                entity.Property(e => e.Faction)
                    .HasMaxLength(32)
                    .HasDefaultValueSql("'rebel'::character varying")
                    .HasColumnName("faction");
                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name");
                entity.Property(e => e.Player).HasColumnName("player");
                entity.Property(e => e.Talent)
                    .HasMaxLength(32)
                    .HasColumnName("talent");

                entity.HasOne(d => d.PlayerNavigation).WithMany(p => p.Decks)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_deck_player");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("game_pkey");

                entity.ToTable("game");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Date)
                    .HasDefaultValueSql("CURRENT_DATE")
                    .HasColumnName("date");
                entity.Property(e => e.Deck).HasColumnName("deck");
                entity.Property(e => e.Opponent)
                    .HasMaxLength(32)
                    .HasColumnName("opponent");
                entity.Property(e => e.Player).HasColumnName("player");
                entity.Property(e => e.Won).HasColumnName("won");

                entity.HasOne(d => d.DeckNavigation).WithMany(p => p.Games)
                    .HasForeignKey(d => d.Deck)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_game_deck");

                entity.HasOne(d => d.PlayerNavigation).WithMany(p => p.Games)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_game_player");
            });

            modelBuilder.Entity<PlayedCard>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("played_cards_pkey");

                entity.ToTable("played_cards");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CardId).HasColumnName("card_id");
                entity.Property(e => e.Player).HasColumnName("player");
                entity.Property(e => e.TimePlayed)
                    .HasDefaultValueSql("1")
                    .HasColumnName("time_played");
                entity.Property(e => e.Victory).HasColumnName("victory");

                entity.HasOne(d => d.PlayerNavigation).WithMany(p => p.PlayedCards)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_playc_game");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("player_pkey");

                entity.ToTable("player");

                entity.HasIndex(e => e.Username, "player_username_key").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BestTrophyScore)
                    .HasDefaultValueSql("0")
                    .HasColumnName("best_trophy_score");
                entity.Property(e => e.Trophies)
                    .HasDefaultValueSql("0")
                    .HasColumnName("trophies");
                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<PlayerStat>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("player_stats");

                entity.Property(e => e.GamePlayed).HasColumnName("game_played");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Loses).HasColumnName("loses");
                entity.Property(e => e.RatioWins).HasColumnName("ratio_wins");
                entity.Property(e => e.TopCards).HasColumnName("top_cards");
                entity.Property(e => e.Wins).HasColumnName("wins");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

