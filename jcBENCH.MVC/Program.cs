using System.Diagnostics;
using jcBENCH.MVC.Configuration;
using jcBENCH.MVC.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace jcBENCH.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddEnvironmentVariables();

            var apiConfig = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();

            if (apiConfig is not null)
            {
                builder.Services.AddSingleton(apiConfig);

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = apiConfig?.JWTIssuer,
                        ValidAudience = apiConfig?.JWTAudience,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiConfig.JWTSecret))
                    };
                });
            }

            builder.Services.AddControllersWithViews();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var configuration = builder.Configuration.GetSection("DbContext");

            if (configuration.Value is null)
            {
                throw new Exception("Configuration not set");
            }

            builder.Services.AddDbContext<MainDbContext>(options => options.UseNpgsql(configuration.Value));

            var app = builder.Build();

            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
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