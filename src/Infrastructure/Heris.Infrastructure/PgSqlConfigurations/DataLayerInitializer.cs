using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heris.Infrastructure.PgSqlConfigurations
{
    public static class DataLayerInitializer
    {
        public static void ConfigurePostgreSql(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<AppPgSqlContext>(cfg =>
            {
                cfg.UseNpgsql(configuration.GetConnectionString("HerisConnectionString"));
            });

            services.AddScoped<BaseDbContext, AppPgSqlContext>();

        }
    }
}
