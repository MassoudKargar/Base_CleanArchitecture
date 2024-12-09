using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heris.Extensions.DependencyInjection;

public static class KafkaProducerServiceCollectionExtensions
{
    public static IServiceCollection AddBaseKafkaProducer(this IServiceCollection services, IConfiguration configuration)
    {        

        return services;
    }

}