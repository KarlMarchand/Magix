namespace magix_api.Repositories
{
    public interface IGameOptionsRepo
    {
        Task<List<Card>> GetAllCards();
        Task<List<Hero>> GetAllHeroes();
        Task<List<Talent>> GetAllTalents();
        Task<List<Faction>> GetAllFactions();
    }
}