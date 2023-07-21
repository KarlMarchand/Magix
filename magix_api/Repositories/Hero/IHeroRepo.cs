namespace magix_api.Repositories
{
    public interface IHeroRepo
    {
        Task<List<Hero>> GetAllHeroes();
        Task<Hero?> GetHeroById(int id);
    }
}
