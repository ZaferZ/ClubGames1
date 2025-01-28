using _8BallPool.Entities;

namespace _8BallPool
{
    public class GameHistoryVM
    {
        public int Id { get; set; }
        public string WinnerName { get; set; }
        public string GameName { get; set; }
        public int TableNumber { get; set; }
        public int GamePoints { get; set; }
        public DateTime EndTime { get; set; }

    }
}
