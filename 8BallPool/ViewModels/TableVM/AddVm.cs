using _8BallPool.Models;
using _8BallPool.Entities;

namespace _8BallPool
{
    public class AddVm
    {
        public int GameId { get; set; }
        public int TableNumber { get; set; }
        public int NumberPlayers { get; set; }
        public int ReservationId { get; set; }
        public List<Game> Games { get; set; }
        public List<GameReseravations> Reseravations { get; set; }
    }
}
