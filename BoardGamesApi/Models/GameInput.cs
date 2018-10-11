using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGamesApi.Models
{
    public class GameInput
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public int Year { get; set; }
        public decimal Rating { get; set; }
        public string Age { get; set; }
        public string Players { get; set; }
        public string PlayingTime { get; set; }
        public string Designer { get; set; }
        public string Publisher { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Image { get; set; }

        public IEnumerable<string> Types { get; set; }

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
