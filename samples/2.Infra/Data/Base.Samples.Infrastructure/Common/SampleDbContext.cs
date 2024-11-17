using Base.Infrastructure.SqlContext;

namespace Base.Samples.Infrastructure.Common;

public class SampleDbContext : BaseDbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}