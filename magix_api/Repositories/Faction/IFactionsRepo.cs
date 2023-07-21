namespace magix_api.Repositories
{
    public interface IFactionsRepo
    {
        public Faction GetFactionByName (string factionName);
        public Task<Faction?> GetFactionById (int id);
        public Task<List<Faction>> GetAllFactions ();
    }
}