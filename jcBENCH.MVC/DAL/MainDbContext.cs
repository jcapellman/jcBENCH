using Microsoft.EntityFrameworkCore;

namespace jcBENCH.MVC.DAL
{
    public class MainDbContext : DbContext
    {
        public DbSet<jcBENCH.MVC.DAL.Objects.Results> BenchmarkResults { get; set; }

        public MainDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}