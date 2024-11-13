using Base.Infrastructure;
using Base.Samples.Core.Contracts.People;
using Base.Samples.Infrastructure.Common;

namespace Base.Samples.Infrastructure.People;

public class PersonCommandRepository(SampleDbContext dbContext)
    : BaseEntityFrameworkRepository<Person, SampleDbContext, long>(dbContext), IPersonRepository
{
}