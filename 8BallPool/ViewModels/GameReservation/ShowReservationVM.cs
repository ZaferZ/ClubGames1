namespace _8BallPool.ViewModels.GameReservation
{
    public class ShowReservationVM
    {
        public List<ReservationVM> Reservations { get; set; } = new List<ReservationVM>();
        public ScoreVM score { get; set; }=new ScoreVM();
    }
}
