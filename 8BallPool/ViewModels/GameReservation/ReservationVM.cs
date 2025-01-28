using _8BallPool.Models;

namespace _8BallPool
{
    public class ReservationVM
    {
        public int Id;
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Game MyGame { get; set; }
        public User MyUser { get; set; }
        public string DaysTillReservation()
        {
            TimeSpan time = StartTime - DateTime.Now;
            if (time.Days < 1)
            {
                return "Today";
            }
            else if (time.Days==1 )
                return "Tomorow";
            else
            {
                return StartTime.Date.ToString("d MMM yyyy");
            }

        }
    }
}
