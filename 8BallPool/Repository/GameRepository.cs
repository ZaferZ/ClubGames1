using _8BallPool.Database;
using _8BallPool.Models;
using System.Linq.Expressions;

namespace _8BallPool.Repository
{
    public class GameRepository
    {
        private ClubGamesProjectDbContext Context;
        public GameRepository()
        {
            Context = new ClubGamesProjectDbContext();
        }
        public Game GetFirstOrDefault(Expression<Func<Game, bool>> filter)
        {
            if (filter == null)
            {
                return Context.Games.OrderBy(g => g.Id).FirstOrDefault();
            }
            else
            return Context.Games.Where(filter).FirstOrDefault();
        }
        public List<Game> GetAll()
        {
            return Context.Games.ToList();
        }
        public void Save(Game game) {
            if (game.Id ==0)
            {
                Context.Games.Add(game);
            }
            else { Context.Games.Update(game);}
            Context.SaveChanges();
        }
        public void Delete(Game game)
        {
            Context.Remove(game);
        }
    }
}
