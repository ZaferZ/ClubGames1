using System.ComponentModel.DataAnnotations;

namespace _8BallPool.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceForHour { get; set; }
        public int WinnerPoints { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

    }
}
