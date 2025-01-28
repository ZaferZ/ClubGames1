using _8BallPool.Entities;
using _8BallPool.Models;
using _8BallPool.ViewModels.Game;

namespace _8BallPool
{
    public class CreateReservationVM
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int TableId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Game> Games { get; set; }
        public List<User> Users { get; set; }
        public List<Table> Tables { get; set; }
    }
}
