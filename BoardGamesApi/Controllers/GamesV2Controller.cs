using BoardGamesApi.Data;
using BoardGamesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoardGamesApi.Controllers
{
    /// <summary>
    /// Games endpoint of Board Games API.
    /// </summary>
    [ApiController]
    [ApiVersion("2")]
    [Authorize]
    [Route("api/v{api-version:apiVersion}/games")]
    public class GamesV2Controller : Controller
    {
        private readonly IGamesRepository _gamesRepository;
        private readonly ILogger<GamesV2Controller> _logger;

        /// <summary>
        /// Creates a new instance of <see cref="GamesV2Controller"/> with dependencies injected.
        /// </summary>
        /// <param name="gamesRepository">A repository for managing the games.</param>
        /// <param name="logger">Logger implementation.</param>
        public GamesV2Controller(IGamesRepository gamesRepository, ILogger<GamesV2Controller> logger)
        {
            _gamesRepository = gamesRepository;
            _logger = logger;
        }

        /// <summary>
        /// Delete the game with the given id.
        /// </summary>
        /// <param name="id">Id of the game to delete.</param>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Get one page of games.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="size">Page size.</param>
        /// <remarks>If you omit <c>page</c> and <c>size</c> query parameters, you'll get the first page with 10 games.</remarks>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PagedList<Game>))]
        [ProducesResponseType(401)]
        public ActionResult<PagedList<Game>> GetAll(int page = 1, int size = 10)
        {
            _logger.LogDebug("Getting one page of games");

            var games = _gamesRepository.GetPage(page, size);

            return games;
        }

        /// <summary>
        /// Get a single game by id.
        /// </summary>
        /// <param name="id">Id of the game to retrieve.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Create a new game from the supplied data.
        /// </summary>
        /// <param name="model">Data to create the game from.</param>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public ActionResult<Game> Post(GameInput model)
        {
            _logger.LogDebug($"Creating a new game with title \"{model.Title}\"");

            var game = new Game();
            model.MapToGame(game);

            _gamesRepository.Create(game);

            return CreatedAtAction(nameof(GetById), "games", new {id = game.Id}, game);
        }

        /// <summary>
        /// Updates the game with the given id.
        /// </summary>
        /// <param name="id">Id of the game to update.</param>
        /// <param name="model">Data to update the game from.</param>
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
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
