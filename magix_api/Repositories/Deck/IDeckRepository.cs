namespace magix_api.Repositories
{
    public interface IDeckRepository
    {
        Task<Deck> CreateDeck(Deck deck);
        Task<bool> DeleteDeck(Guid deckId, int requesterId);
        Task<List<Deck>> GetAllDecks(int playerId);
        Task<Deck?> GetDeck(Guid deckId);
        Task<Deck?> GetActiveDeck(int playerId);
        Task<Deck> UpdateDeck(Deck deck);
        Task UpdateActiveDeck(Deck deck);
    }
}