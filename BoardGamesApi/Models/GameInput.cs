using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGamesApi.Models
{
    /// <summary>
    /// Represents a single board game data to be saved.
    /// </summary>
    public class GameInput
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the release year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the Board Game Geek rating.
        /// </summary>
        public decimal Rating { get; set; }

        /// <summary>
        /// Gets or sets the player age information.
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Gets or sets the number of players information.
        /// </summary>
        public string Players { get; set; }

        /// <summary>
        /// Gets or sets the playing time information.
        /// </summary>
        public string PlayingTime { get; set; }

        /// <summary>
        /// Gets or sets the designer names.
        /// </summary>
        public string Designer { get; set; }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the Board Game Geek URL.
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the Board Game Geek image URL.
        /// </summary>
        [Required]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the list of game types.
        /// </summary>
        public IEnumerable<string> Types { get; set; }

        /// <summary>
        /// Copy data to <see cref="Game"/> instance.
        /// </summary>
        /// <param name="game">Game to copy the input data to.</param>
        public void MapToGame(Game game)
        {
            game.Title = Title;
            game.Year = Year;
            game.Rating = Rating;
            game.Age = Age;
            game.Players = Players;
            game.PlayingTime = PlayingTime;
            game.Designer = Designer;
            game.Publisher = Publisher;
            game.Url = Url;
            game.Image = Image;
            game.Types = Types;
        }
    }
}
