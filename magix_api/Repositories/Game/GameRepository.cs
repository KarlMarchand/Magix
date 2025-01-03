using magix_api.Data;
using magix_api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace magix_api.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly MagixContext _context;

        public GameRepository(MagixContext context)
        {
            _context = context;
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            var entry = await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<PaginatedResponse<Game>> GetGamesHistoryAsync(int playerId, int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            // Fetch the total number of items
            int totalItems = await _context.Games.CountAsync(game => game.PlayerId == playerId);

            // Retrieve the paginated data
            var games = await _context.Games
                                      .Where(game => game.PlayerId == playerId)
                                      .OrderByDescending(game => game.Date)
                                      .Skip(skip)
                                      .Take(pageSize)
                                      .ToListAsync();

            return new PaginatedResponse<Game>(games, totalItems, pageNumber, pageSize);
        }

        public async Task AddPlayedCardsAsync(int playerId, bool isVictory, List<int> playedCardsIds)
        {
            foreach (var cardId in playedCardsIds)
            {
                var playedCard = await _context.PlayedCards
                    .FirstOrDefaultAsync(pc => pc.PlayerId == playerId && pc.CardId == cardId);

                if (playedCard != null)
                {
                    // The combination of player and card exists, so update it
                    playedCard.TimePlayed += 1;
                    if (isVictory)
                    {
                        playedCard.Victory += 1;
                    }
                }
                else
                {
                    // The combination does not exist, so create a new record
                    playedCard = new PlayedCard
                    {
                        PlayerId = playerId,
                        CardId = cardId,
                        TimePlayed = 1,
                        Victory = isVictory ? 1 : 0
                    };
                    _context.PlayedCards.Add(playedCard);
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}