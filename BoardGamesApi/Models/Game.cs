using System.Collections.Generic;

namespace BoardGamesApi.Models
{
    /// <summary>
    /// Represents a single board game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Gets or sets the game id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
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
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the Board Game Geek image URL.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the list of game types.
        /// </summary>
        public IEnumerable<string> Types { get; set; }
    }
}
