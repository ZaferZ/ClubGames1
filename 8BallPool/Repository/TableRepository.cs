using _8BallPool.Database;
using _8BallPool.Entities;
using _8BallPool.Models;
using System.Linq.Expressions;

namespace _8BallPool.Repository
{
    public class TableRepository
    {
        private ClubGamesProjectDbContext Context;
        public TableRepository()
        {
            Context = new ClubGamesProjectDbContext();
        }
        public Table GetFirstOrDefault(Expression<Func<Table, bool>> filter)
        {
            return Context.Tables.Where(filter).FirstOrDefault();
        }
        public List<Table> GetAll()
        {
            GameRepository games = new();
           List<Table> tables =  Context.Tables.ToList();
            foreach (var item in tables)
            {
                item.Game = games.GetFirstOrDefault(x=> x.Id == item.GameId);
            }
            return tables;
        }
        public void Save(Table table)
        {
            if (table.Id != 0)
            {
                Context.Tables.Update(table);
            }
            else
            {
                Context.Tables.Add(table);
            }
            Context.SaveChanges();
        }
        public void Delete(Table table)
        {
            Context.Remove(table);
            Context.SaveChanges();
        }
    }
}
