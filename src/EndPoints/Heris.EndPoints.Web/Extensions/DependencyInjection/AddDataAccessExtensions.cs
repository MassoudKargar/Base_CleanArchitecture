namespace Heris.EndPoints.Web.Extensions.DependencyInjection;

/// <summary>
/// توابع کمکی جهت ثبت نیازمندی‌های لایه داده
/// </summary>
public static class AddDataAccessExtensions
{
    /// <summary>
    /// اضافه می‌کند سرویس‌های لایه داده را به IServiceCollection.
    /// </summary>
    /// <param name="services">Collecting services to add.</param>
    /// <param name="assembliesForSearch">Assemblies to search for repository and unit of work implementations.</param>
    /// <returns>The modified IServiceCollection with added data access services.</returns>
    public static IServiceCollection AddBaseDataAccess(this IServiceCollection services, Assembly[] assembliesForSearch)
    {
        // Adds repositories and unit of work services
        return services.AddRepositories(assembliesForSearch).AddUnitOfWorks(assembliesForSearch);
    }

    /// <summary>
    /// اضافه می‌کند سرویس‌های رابطه با داده‌ها به IServiceCollection با زمان حفظ حرفه‌ای.
    /// </summary>
    /// <param name="services">Collecting services to add.</param>
    /// <param name="assembliesForSearch">Assemblies to search for repository implementations.</param>
    /// <returns>The modified IServiceCollection with added repository services.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services,
        Assembly[] assembliesForSearch)
    {
        // Adds repositories with transient lifetime from specified assemblies
        return services.AddWithTransientLifetime(assembliesForSearch, typeof(IGenericRepository<,>));
    }

    /// <summary>
    /// اضافه می‌کند سرویس‌های واحدهای کار به IServiceCollection با زمان حفظ حرفه‌ای.
    /// </summary>
    /// <param name="services">Collecting services to add.</param>
    /// <param name="assembliesForSearch">Assemblies to search for unit of work implementations.</param>
    /// <returns>The modified IServiceCollection with added unit of work services.</returns>
    public static IServiceCollection AddUnitOfWorks(this IServiceCollection services,
        Assembly[] assembliesForSearch)
    {
        // Adds unit of work with transient lifetime from specified assemblies
        return services.AddWithTransientLifetime(assembliesForSearch, typeof(IUnitOfWork));
    }

    /// <summary>
    /// اضافه می‌کند سرویس‌های بهینه‌سازی شده به IServiceCollection با زمان حفظ حرفه‌ای.
    /// </summary>
    /// <param name="services">Collecting services to add.</param>
    /// <param name="assembliesForSearch">Assemblies to search for implementations of the specified interface.</param>
    /// <param name="serviceType">The type of service to register.</param>
    /// <returns>The modified IServiceCollection with added services.</returns>
    private static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
       Assembly[] assembliesForSearch, Type serviceType)
    {
        // Searches for implementations of the specified interface in the given assemblies
        var implementationTypes = assembliesForSearch
            .SelectMany(a => a.GetExportedTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false } && serviceType.IsAssignableFrom(t));

        foreach (var type in implementationTypes)
        {
            // Registers each found implementation with transient lifetime
            services.AddTransient(serviceType, type);
        }

        return services;
    }
}