using _8BallPool.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8BallPool.Entities
{
    public class GameHistory
    {
        [Key]
        public int Id { get; set; }
        public int WinnerId { get; set; }
        public int GameId { get; set; }
        public int TableId { get; set; }
        [ForeignKey("WinnerId")]
        public virtual User Winner { get; set; }
        [ForeignKey(nameof(TableId))]
        public virtual Table Table { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }
}
