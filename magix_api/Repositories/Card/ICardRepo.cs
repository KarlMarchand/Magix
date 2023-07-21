namespace magix_api.Repositories
{
    public interface ICardRepo
    {
        Task<List<Card>> GetAllCards();
        Task<Card?> GetCardById(int id);
    }
}
