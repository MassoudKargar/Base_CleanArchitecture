using Base.Sample.Infrastructure.Ef.Context;
using Base.Samples.Core.Domain.People.Entities;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Ef.PostgreSQL
{
    public class AppPgSqlContext : BaseDbContext
    {
        public AppPgSqlContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }
        public DbSet<Person> Persons { get; set; }
    }
}
