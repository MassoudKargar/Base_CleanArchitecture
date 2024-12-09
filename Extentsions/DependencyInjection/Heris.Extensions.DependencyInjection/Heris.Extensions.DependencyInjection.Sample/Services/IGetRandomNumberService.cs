namespace Heris.Extensions.DependencyInjection.Sample.Services;

public interface IGetGuidTransientService : ITransientLifetime
{
    Guid Execute();
}