namespace Heris.Extensions.DependencyInjection.Sample.Services;

public interface IGetGuidSingletonService : ISingletonLifetime
{
    Guid Execute();
}
