using Ardalis.Result;

namespace Base.EndPoints.Web.Extensions.DependencyInjection;

public static class AddApiConfigurationExtensions
{
    public static IServiceCollection AddBaseApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
    {
        services.AddControllers();
        services.AddArdalis();
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddBaseDependencies(assemblyNamesForLoad);

        return services;
    }

    public static IServiceCollection AddArdalis(this IServiceCollection services)
    {
        services.AddControllers(mvcOptions => mvcOptions
            .AddResultConvention(resultStatusMap => resultStatusMap
                .AddDefaultMap()
                .For(ResultStatus.Ok, HttpStatusCode.OK, resultStatusOptions => resultStatusOptions
                    .For("POST", HttpStatusCode.Created)
                    .For("DELETE", HttpStatusCode.NoContent))
                .Remove(ResultStatus.Forbidden)
                .Remove(ResultStatus.Unauthorized)
            ));
        return services;
    }
}