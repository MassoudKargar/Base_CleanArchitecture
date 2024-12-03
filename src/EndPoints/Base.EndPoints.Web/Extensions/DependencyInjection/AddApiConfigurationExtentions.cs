using Base.Infra.Validator;
using Base.Sample.Application.People.Validators;
using Base.Sample.Application.People.ViewModels;
using Microsoft.AspNetCore.OData;

namespace Base.EndPoints.Web.Extensions.DependencyInjection;

public static class AddApiConfigurationExtensions
{
    public static IServiceCollection AddBaseApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
    {
        services.AddControllers().AddOData(options => options.EnableQueryFeatures());
        services.AddBaseDependencies(assemblyNamesForLoad);
        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services, Assembly asm, string vmNameSpace)
    {
        services.InitializeValidator(asm, vmNameSpace);
        return services;
        
    }
}