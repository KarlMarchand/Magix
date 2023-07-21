namespace magix_api.Repositories
{
    public interface IGameRepository
    {
        Task<Game> CreateGame(Game game);
    }
}