using BoardGamesApi.Models;

namespace BoardGamesApi.Data
{
    public interface IGamesRepository
    {
        PagedList<Game> GetPage(int page = 1, int pageSize = 10);
        Game GetById(string id);
        void Create(Game game);
        void Delete(string id);
        void Update(Game game);
    }
}
