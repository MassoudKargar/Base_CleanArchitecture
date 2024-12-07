using Base.Samples.Core.Domain.People.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Sample.Infrastructure.Ef.Configs.EntityTypeConfigurations
{
    public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x=>x.FirstName).HasMaxLength(FieldSizes.Mid_Small);
            builder.Property(x=>x.LastName).HasMaxLength(FieldSizes.Mid_Small);
        }
    }
}
