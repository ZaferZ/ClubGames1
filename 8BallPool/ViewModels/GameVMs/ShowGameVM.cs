
namespace _8BallPool.ViewModels.Game
{
    public class ShowGameVM
    {
        public List<PostGame> Games { get; set; } = new List<PostGame>();
        public ScoreVM ScoreVM { get; set; } = new ScoreVM();
    }
}
