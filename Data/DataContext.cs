using JournalistTierAPI.Model;
using Microsoft.EntityFrameworkCore;
namespace JournalistTierAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Journalist> Journalist { get; set; }

        public DbSet<Media> Media { get; set; }

        public DbSet<Topic> Topic { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserJournalistRating> UserJournalistRating { get; set; }

        public DbSet<UserMediaRating> UserMediaRating { get; set; }
    }
}