﻿namespace Base.EndPoints.Web.Extensions.DependencyInjection;

public static class AddApiConfigurationExtensions
{
    public static IServiceCollection AddBaseApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
    {
        services.AddControllers();
        services.AddBaseDependencies(assemblyNamesForLoad);
        return services;
    }
}