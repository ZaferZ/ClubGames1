using _8BallPool.Entities;
using _8BallPool.Models;

namespace _8BallPool
{
    public class EditVM
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int TableNumber { get; set; }
        public int NumberPlayers { get; set; }
        public List<Game> Games { get; set; }
    }
}
