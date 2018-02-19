using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace jcBENCH.web.DAL
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=XX;user id=XX;password=123456");

            return new MainDbContext(optionsBuilder.Options);
        }
    }
}