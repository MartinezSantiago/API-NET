using ChallengeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

       

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ChallengeApi;Integrated Security=True;");
        }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
    }
}