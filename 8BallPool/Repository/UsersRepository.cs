using _8BallPool.Database;
using _8BallPool.Models;
using System.Linq;
using System.Linq.Expressions;

namespace _8BallPool.Repository
{
    public class UsersRepository
    {
        private ClubGamesProjectDbContext Context;
        public UsersRepository()
        { 
            Context = new ClubGamesProjectDbContext();
        }
        public User GetFirstOrDefault(Expression<Func<User, bool>> filter)
        { 
        return Context.Users.Where(filter).FirstOrDefault();
        }
        public List<User> GetAll()
        {
            return Context.Users.ToList();
        }
        public List<User> GetAllDecc(Expression<Func<User,int>> keySelector)
        {
            return Context.Users.OrderByDescending(keySelector).ToList();
        }
        public void Save(User user)
        {
            if (user.Id != 0)
            {
                Context.Users.Update(user);
            }
            else
            { 
                Context.Users.Add(user);
            }
            Context.SaveChanges();
        }
        public void Delete(User user)
        {
            Context.Remove(user);
            Context.SaveChanges();
        }
    }
}
