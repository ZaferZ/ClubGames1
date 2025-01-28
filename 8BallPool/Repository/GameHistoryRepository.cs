using _8BallPool.Database;
using _8BallPool.Entities;
using _8BallPool.Models;

namespace _8BallPool.Repository
{
    public class GameHistoryRepository
    {
        private ClubGamesProjectDbContext Context;
        public GameHistoryRepository()
        {
            Context = new ClubGamesProjectDbContext();
        }
        public void Save(GameHistory game)
        {
            if (game.Id == 0)
            {
                Context.GameHistories.Add(game);
            }
            else { Context.GameHistories.Update(game); }
            Context.SaveChanges();
        }
        public List<GameHistory> GetAll()
        {
            return Context.GameHistories.ToList();
        }
    }
}
    

