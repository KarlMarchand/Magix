namespace magix_api.Repositories
{
    public interface ITalentRepo
    {
        Task<List<Talent>> GetAllTalents();
        Task<Talent?> GetTalentById(int id);
        Task<Talent?> GetTalentByName(string name);
    }
}
