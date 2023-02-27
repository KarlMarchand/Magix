using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace magix_api.Data
{
    public partial class MagixContext : DbContext
    {
        public MagixContext(DbContextOptions<MagixContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards => Set<Card>();
        public DbSet<Deck> Decks => Set<Deck>();
        public DbSet<Faction> Factions => Set<Faction>();
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Hero> Heroes => Set<Hero>();
        public DbSet<PlayedCard> PlayedCards => Set<PlayedCard>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Talent> Talents => Set<Talent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .Property(e => e.Mechanics)
                .HasConversion(new CsvListConverter());
        }
    }

    public class CsvListConverter : ValueConverter<List<string>?, string>
    {
        public CsvListConverter() : base(
            v => v == null ? string.Empty : string.Join(",", v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
        ){}
    }
}

