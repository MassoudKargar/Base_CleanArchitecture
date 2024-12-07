using Base.Infrastructure.Ef.Context;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.PostgreSQL
{
    public class AppPgSqlContext : BaseDbContext
    {
        public AppPgSqlContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

    }
}
