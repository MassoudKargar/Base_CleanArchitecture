using Base.Sample.BackgroundWorker.LocationService;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Sample.BackgroundWorker.DependencyInjection;
/// <summary>
/// توابع کمکی جهت ثبت نیازمندی‌های لایه Background Worker
/// </summary>
public static class AddWorkerExtensions
{

    public static IServiceCollection AddBackgroundWorkerDependencies(this IServiceCollection services)
    {
        services.AddHostedService<LocationConsumerService>();
        return services;
    }
}
