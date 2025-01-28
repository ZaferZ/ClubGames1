using _8BallPool.Models;

namespace _8BallPool
{
    public class EndGameVM
    {
        public int WinnerId {  get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int ReservationId { get; set; }
        public int GamePoints { get; set; }
        public int TableId { get; set; }
        public List<User> Users { get; set; }
    }
}
