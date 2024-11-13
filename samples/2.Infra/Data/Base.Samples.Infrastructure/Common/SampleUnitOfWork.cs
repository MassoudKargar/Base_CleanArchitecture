using Base.Infrastructure;
using Base.Samples.Core.Contracts.Common;

namespace Base.Samples.Infrastructure.Common;

public class SampleUnitOfWork(SampleDbContext dbContext) : BaseEntityFrameworkUnitOfWork<SampleDbContext>(dbContext), ISampleUnitOfWork
{

}