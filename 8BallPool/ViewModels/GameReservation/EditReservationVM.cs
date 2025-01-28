using _8BallPool.Models;

namespace _8BallPool
{
    public class EditReservationVM
    {
        public int Id;
        public int GameId { get; set; }
        public string? GameName { get; set; }
        public int PlayerId { get; set; }
        public string? PlayerName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
        public List<User> Users { get; set; }=new List<User>();

    }
}
