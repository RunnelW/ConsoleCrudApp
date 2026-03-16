using Microsoft.EntityFrameworkCore;
using MovieDiary.Console.Models;

namespace MovieDiary.Console.EF
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MovieDiary"].ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}