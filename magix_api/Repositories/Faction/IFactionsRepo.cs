namespace magix_api.Repositories
{
    public interface IFactionsRepo
    {
        public Faction GetFaction (string factionName);

        public List<Faction> GetAllFactions ();
    }
}