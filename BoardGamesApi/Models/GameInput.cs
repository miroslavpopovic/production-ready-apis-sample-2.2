using System.Collections.Generic;

namespace BoardGamesApi.Models
{
    public class GameInput
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Rating { get; set; }
        public string Age { get; set; }
        public string Players { get; set; }
        public string PlayingTime { get; set; }
        public string Designer { get; set; }
        public string Publisher { get; set; }
        public string Url { get; set; }
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
