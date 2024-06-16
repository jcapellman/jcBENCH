using jcBENCH.MVC.DAL;
using Microsoft.EntityFrameworkCore;

namespace jcBENCH.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddControllersWithViews();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOutputCache();

            var configuration = builder.Configuration.GetSection("DbContext");

            if (configuration.Value is null)
            {
                throw new Exception("Configuration not set");
            }

            builder.Services.AddDbContext<MainDbContext>(options => options.UseNpgsql(configuration.Value));

            var app = builder.Build();

            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MainDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
