using Microsoft.AspNetCore.OData;

namespace Base.EndPoints.Web.Extensions.DependencyInjection;

public static class AddApiConfigurationExtensions
{
    public static IServiceCollection AddBaseApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
    {
        services.AddControllers().AddOData(
            options => options.EnableQueryFeatures());
        services.AddBaseDependencies(assemblyNamesForLoad);
        return services;
    }
}