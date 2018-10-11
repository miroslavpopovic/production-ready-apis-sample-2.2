using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BoardGamesApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BoardGamesApi.Data
{
    public class GamesRepository : IGamesRepository
    {
        private IList<Game> _games;

        public IEnumerable<Game> GetAll()
        {
            var games = GetGames();

            return games;
        }

        public Game GetById(string id)
        {
            var games = GetGames();

            return games.FirstOrDefault(x => x.Id == id);
        }

        public void Create(Game game)
        {
            game.Id = GetNextId();
            GetGames().Add(game);
        }

        public void Delete(string id)
        {
            var games = GetGames();
            var gameToDelete = games.First(x => x.Id == id);

            games.Remove(gameToDelete);
        }

        public void Update(Game game)
        {
            var games = GetGames();
            var gameToUpdate = games.First(x => x.Id == game.Id);

            games.Remove(gameToUpdate);
            games.Add(game);
        }

        private IList<Game> GetGames()
        {
            if (_games == null)
            {
                var assembly = Assembly.GetEntryAssembly();
                var resourceStream = assembly.GetManifestResourceStream("BoardGamesApi.Data.games.json");

                using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var gamesArray = JObject.Load(jsonReader)["games"];
                    _games = gamesArray.ToObject<IList<Game>>();
                }
            }

            return _games;
        }

        private string GetNextId()
        {
            var maxId = GetGames().Select(x => int.Parse(x.Id.Split("-")[1])).Max();
            return $"gam-{maxId + 1:D6}";
        }
    }
}
