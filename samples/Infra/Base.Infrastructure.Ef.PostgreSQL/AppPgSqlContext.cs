using Base.Sample.Infrastructure.Ef.Configs.EntityTypeConfigurations;
using Base.Sample.Infrastructure.Ef.Context;
using Base.Samples.Core.Domain.People.Entities;
using Microsoft.EntityFrameworkCore;

namespace Base.Sample.Infrastructure.Ef.PostgreSQL
{
    public class AppPgSqlContext : BaseDbContext
    {
        public AppPgSqlContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
        }
        public DbSet<Person> Persons { get; set; }
    }
}
