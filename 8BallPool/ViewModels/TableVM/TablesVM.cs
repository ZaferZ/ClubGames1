namespace _8BallPool.ViewModels.TableVM
{
    public class TablesVM
    {
        public int Id { get; set; }
        public int TableName { get; set; }
        public string GameName { get; set; }
        public int NumberOfPlayers { get; set; }
        public decimal PriceForHour { get; set; }
        public int Points { get; set; }
        public bool IsReserved { get; set;}
    }
}
