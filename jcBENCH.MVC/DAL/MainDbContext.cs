using jcBENCH.MVC.DAL.Objects;
using Microsoft.EntityFrameworkCore;

namespace jcBENCH.MVC.DAL
{
    public class MainDbContext : DbContext
    {
        public DbSet<ReleaseArtifacts> ReleaseArtifacts { get; set; }

        public DbSet<Releases> Releases { get; set; }

        public DbSet<jcBENCH.MVC.DAL.Objects.Results> BenchmarkResults { get; set; }

        public MainDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}