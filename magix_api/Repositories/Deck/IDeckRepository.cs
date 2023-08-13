namespace magix_api.Repositories
{
    public interface IDeckRepository
    {
        Task<Deck> CreateDeck(Deck deck);
        Task<bool> DeleteDeck(Guid deckId, int requesterId);
        Task<List<Deck>> GetAllDecks(int playerId);
        Task<Deck?> GetDeck(Guid? deckId = null, int? playerId = null);
        Task<Deck> UpdateDeck(Deck deck);
        Task UpdateActiveDeck(Deck deck);
    }
}