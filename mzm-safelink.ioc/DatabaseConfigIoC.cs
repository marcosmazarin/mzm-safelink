using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using mzm_safelink.domain.entities;
using mzm_safelink.domain.interfaces;
using mzm_safelink.infra.persistence;

namespace mzm_safelink.ioc
{
    public static class DatabaseConfigIoC
    {
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services)
        {
            // Register the DbContext with PostgreSQL
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("SUPABASE")!));
            
            services.AddScoped<IBaseRepository<Url>, BaseRepository<Url>>();
            services.AddScoped<IUrlRepository, UrlRepository>();

            return services;
        }
    }
}