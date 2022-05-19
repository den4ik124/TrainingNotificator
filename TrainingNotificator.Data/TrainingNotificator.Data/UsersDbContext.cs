using Microsoft.EntityFrameworkCore;
using TrainingNotificator.Core.Models;

namespace TrainingNotificator.Data
{
    public class UsersDbContext : DbContext
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrainingNotificator;Integrated Security=True";

        public DbSet<CustomUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}