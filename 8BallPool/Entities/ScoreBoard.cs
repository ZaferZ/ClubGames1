using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8BallPool.Models
{
    public class ScoreBoard
    {
        public string UserName { get; set; }
        public int Score { get; set; }

    }
}
