namespace Heris.EndPoints.Web.Extensions.DependencyInjection;
public static class FluentValidationExtenstion
{
    public static void InitializeValidator(this IServiceCollection services)
        => services.AddFluentValidationAutoValidation();

    public static void RegisterValidator<TModel, TValidator>(this IServiceCollection services) where TValidator : AbstractValidator<TModel>
        where TModel : AbstractValidator<TModel>
    {
        services.AddScoped<IValidator<TModel>, TValidator>();

    }
    public static void InitializeValidator(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        InitializeValidator(services);
        RegisterValidatorsByAssembly(services, assemblies);
    }

    public static void RegisterValidatorsByAssembly(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblies(assemblies);
    }    // تابع کمکی برای بررسی ارث‌بری از AbstractValidator<>
}