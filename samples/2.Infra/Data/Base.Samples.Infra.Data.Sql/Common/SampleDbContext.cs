namespace Base.Samples.Infra.Data.Sql.Commands.Common;
using Base.Infra.Data.Sql;

public class SampleDbContext : BaseDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
    public DbSet<Person> People { get; set; }
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
    {
    }
}