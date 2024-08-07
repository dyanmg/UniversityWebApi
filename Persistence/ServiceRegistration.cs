using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Repositories;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSqlServer<SchoolContext>(configuration.GetConnectionString("Training"),
                x => x.MigrationsHistoryTable("_migrationsHistory", "gufron"));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static void Migrate(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<SchoolContext>();
                context.Database.Migrate();
                DbInitializer.Initialize(context);
            }
        }
    }
}
