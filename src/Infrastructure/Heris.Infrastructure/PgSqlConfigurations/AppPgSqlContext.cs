namespace Heris.Infrastructure.PgSqlConfigurations;
public class AppPgSqlContext : BaseDbContext
{
    public AppPgSqlContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}