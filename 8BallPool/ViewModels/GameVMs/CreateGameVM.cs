namespace _8BallPool.ViewModels.Game
{
    public class CreateGameVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceForHour { get; set; }
        public int WinnerPoints { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
