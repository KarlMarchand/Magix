namespace magix_api.Repositories
{
    public interface ICardRepo
    {
        Task<List<Card>> GetAllCards();
        Task<Card?> GetCardById(int id);
        Task<List<Card>> GetCardsByIds(List<int> cardIds);
    }
}
