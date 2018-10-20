using BoardGamesApi.Data;
using BoardGamesApi.Models;

namespace BoardGamesApi.Tests.UnitTests
{
    public class FakeGamesRepository : IGamesRepository
    {
        public PagedList<Game> GetPage(int page = 1, int pageSize = 10)
        {
            return new PagedList<Game>();
        }

        public Game GetById(string id)
        {
            return id == "existing" ? new Game { Id = id } : null;
        }

        public void Create(Game game)
        {
        }

        public void Delete(string id)
        {
        }

        public void Update(Game game)
        {
        }
    }
}
