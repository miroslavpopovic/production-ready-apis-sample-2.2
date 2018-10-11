using BoardGamesApi.Data;
using BoardGamesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoardGamesApi.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly IGamesRepository _gamesRepository;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGamesRepository gamesRepository, ILogger<GamesController> logger)
        {
            _gamesRepository = gamesRepository;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _logger.LogDebug($"Deleting game with id {id}");

            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var game = _gamesRepository.GetById(id);
            if (game == null)
                return NotFound();

            _gamesRepository.Delete(id);

            return Ok();
        }

        [HttpGet]
        public ActionResult<PagedList<Game>> GetAll(int page = 1, int size = 10)
        {
            _logger.LogDebug("Getting one page of games");

            var games = _gamesRepository.GetPage(page, size);

            return games;
        }

        [HttpGet("{id}")]
        public ActionResult<Game> GetById(string id)
        {
            _logger.LogDebug($"Getting a game with id {id}");

            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var game = _gamesRepository.GetById(id);

            if (game == null)
                return NotFound();

            return game;
        }

        [HttpPost]
        public ActionResult<Game> Post(GameInput model)
        {
            _logger.LogDebug($"Creating a new game with title \"{model.Title}\"");

            var game = new Game();
            model.MapToGame(game);

            _gamesRepository.Create(game);

            return CreatedAtAction(nameof(GetById), "games", new {id = game.Id}, game);
        }

        [HttpPut("{id}")]
        public ActionResult<Game> Put(string id, GameInput model)
        {
            _logger.LogDebug($"Updating a game with id {id}");

            var game = _gamesRepository.GetById(id);

            if (game == null)
                return NotFound();

            model.MapToGame(game);

            _gamesRepository.Update(game);

            return game;
        }
    }
}
