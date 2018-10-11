using System.Collections.Generic;

namespace BoardGamesApi.Models
{
    public class Game
    {
        public string Id { get; set; }
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
    }
}
