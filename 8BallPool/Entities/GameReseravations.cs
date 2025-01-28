using _8BallPool.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8BallPool.Entities
{
    public class GameReseravations
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int TablesId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ForeignKey("PlayerId")]
        public virtual User Player { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(TablesId))]
        public virtual Table Table { get; set; }

    }
}
