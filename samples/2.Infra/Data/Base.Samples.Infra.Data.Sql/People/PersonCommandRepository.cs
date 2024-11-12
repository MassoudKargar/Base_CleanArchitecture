using Base.Infra.Data.Sql;
using Base.Samples.Core.Contracts.People;
using Base.Samples.Infra.Data.Sql.Commands.Common;

namespace Base.Samples.Infra.Data.Sql.Commands.People;

public class PersonCommandRepository(SampleDbContext dbContext) 
    : BaseEntityFrameworkRepository<Person, SampleDbContext,long>(dbContext), IPersonRepository
{
}