using _8BallPool.Database;
using _8BallPool.Entities;
using _8BallPool.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _8BallPool.Repository
{
    public class GameReservationRepository
    {
        private ClubGamesProjectDbContext Context;
        public GameReservationRepository()
        {
            Context = new ClubGamesProjectDbContext();
        }
        public GameReseravations GetFirstOrDefault(Expression<Func<GameReseravations, bool>> filter)
        {
            if (filter == null)
            {
                return Context.GameReservations.OrderBy(g => g.Id).FirstOrDefault();
            }
            else
                return Context.GameReservations.Where(filter).FirstOrDefault();
        }
        public List<GameReseravations> GetAll()
        {
            return Context.GameReservations.ToList();
        }
        public void Save(GameReseravations game)
        {
            if (game.Id == 0)
            {
                Context.GameReservations.Add(game);
               // Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ClubGamesProject.Games ON;");
                Context.SaveChanges();

            }
            else { Context.GameReservations.Update(game);
                Context.SaveChanges();
            }
         
        }
        public void Delete(GameReseravations game)
        {
            Context.Remove(game);
            Context.SaveChanges();
        }
    }
}
