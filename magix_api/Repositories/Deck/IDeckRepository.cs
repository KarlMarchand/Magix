namespace magix_api.Repositories
{
    public interface IDeckRepository
    {
        Task<Deck> CreateDeck(Deck deck);
        Task<bool> DeleteDeck(Deck deck);
        Task<List<Deck>> GetAllDecks(int playerId);
        Task<Deck> GetDeck(int deckId);
        Task<Deck> UpdateDeck(Deck deck);
    }
}