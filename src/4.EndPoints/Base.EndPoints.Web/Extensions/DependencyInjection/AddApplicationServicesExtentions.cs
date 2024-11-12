namespace Base.EndPoints.Web.Extensions.DependencyInjection;

public static class AddApplicationServicesExtensions
{
    public static IServiceCollection AddBaseApplicationServices(this IServiceCollection services,
                                                                 IEnumerable<Assembly> assembliesForSearch)
        => services.AddFluentValidators(assembliesForSearch);

    private static IServiceCollection AddFluentValidators(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddValidatorsFromAssemblies(assembliesForSearch);
}

