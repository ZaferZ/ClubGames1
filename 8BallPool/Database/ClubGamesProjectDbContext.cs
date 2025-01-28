using _8BallPool.Entities;
using _8BallPool.Models;
using Microsoft.EntityFrameworkCore;

namespace _8BallPool.Database
{
    public class ClubGamesProjectDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameHistory> GameHistories { get; set; }
        public DbSet<GameReseravations> GameReservations { get; set; }
        public DbSet<Table> Tables { get; set; }
        public ClubGamesProjectDbContext()
        {
            this.Users = Set<User>();
            this.Games = Set<Game>();
            this.GameHistories = Set<GameHistory>();
            this.GameReservations = Set<GameReseravations>();
            this.Tables = Set<Table>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ClubGamesProject2;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Zafer Bayraktarov",
                    Password = "zaferPass",
                    UserName = "ZaferB",
                    Points = 0,
                    Email = "zafer.bayraktarov@gmail.com",
                    IsAdmin = true
                }
                );

        }
    }
}
