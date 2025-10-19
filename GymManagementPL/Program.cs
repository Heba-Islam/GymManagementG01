using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Data.DataSeeding;
using GymManagementDAL.Repositories.implementation;
using GymManagementDAL.Repositories.Implementation;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });

            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            //builder.Services.AddScoped<ISessionRepository, SessionRepository>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(ISessionRepository), typeof(SessionRepository));


            var app = builder.Build();

            #region DataSeed 
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }
            GymDBContextSeeding.DataSeed(dbContext);
            #endregion


            #region pipeline configs
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets(); 
            #endregion

            app.Run();
        }
    }
}
