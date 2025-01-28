using _8BallPool.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8BallPool.Entities
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int TableNumber { get; set; }
        public int NumberPlayers { get; set; }
        public bool IsAvailable { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }

    }
}
