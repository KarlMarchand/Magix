namespace magix_api.Repositories
{
    public interface IDeckRepository
    {
        Task<Deck> AddDeck(int playerId, Deck deck);
        Task<Deck> DeleteDeck(int playerId, Deck deck);
        List<Deck> GetAllDecks(int playerId);
        Task<Deck> GetDeck(int playerId);
        Task<Deck> SaveDeck(int playerId, Deck deck);
        Task<Deck> SwitchDeck(int id, int playerId);
    }
}