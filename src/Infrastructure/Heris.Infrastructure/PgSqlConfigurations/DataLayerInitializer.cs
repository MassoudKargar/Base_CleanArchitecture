namespace Heris.Infrastructure.PgSqlConfigurations;

public static class DataLayerInitializer
{
    public static void ConfigurePostgreSql<T>(this IServiceCollection services, IConfiguration configuration) where T : BaseDbContext
    {
        services.AddDbContext<T>(cfg =>
        {
            cfg.UseNpgsql(configuration.GetConnectionString("HerisConnectionString"), options =>
            {
                options.MigrationsAssembly(typeof(T).Assembly.GetName().Name);
            });
        });

        services.AddScoped<BaseDbContext, T>();

    }
}