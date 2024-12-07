
using Base.Core.Contracts.Data;
using Base.Infrastructure.Ef.Context;
using Base.Infrastructure.Ef.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure.PostgreSQL
{
    public static class DataLayerInitializer
    {
        public static void ConfigurePostgreSql(this IServiceCollection services, string constr)
        {
            services.AddDbContext<AppPgSqlContext>(cfg =>
            {
                cfg.UseNpgsql(constr);
            });

            services.AddScoped<BaseDbContext, AppPgSqlContext>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        public static void InjectRepositories()
        {

        }
    }
}
