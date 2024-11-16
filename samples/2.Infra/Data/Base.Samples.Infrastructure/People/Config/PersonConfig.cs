namespace Base.Samples.Infrastructure.People.Config;

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.FirstName).HasMaxLength(100);
        builder.HasOne(o => o.Store).WithMany(s => s.Persons).HasForeignKey(f => f.StoreId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}