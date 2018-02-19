using jcBENCH.web.DAL.Objects;

using Microsoft.EntityFrameworkCore;

namespace jcBENCH.web.DAL
{
    public class MainDbContext : DbContext
    {
        public DbSet<Benchmarks> Benchmarks { get; set; }

        public DbSet<Results> Results { get; set; }

        public DbSet<Platforms> Platforms { get; set; }

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}