using Base.Infra.Data.Sql.Commands;
using Base.Samples.Core.Contracts.Common;

namespace Base.Samples.Infra.Data.Sql.Commands.Common;

public class SampleUnitOfWork(SampleDbContext dbContext) : BaseEntityFrameworkUnitOfWork<SampleDbContext>(dbContext), ISampleUnitOfWork
{

}