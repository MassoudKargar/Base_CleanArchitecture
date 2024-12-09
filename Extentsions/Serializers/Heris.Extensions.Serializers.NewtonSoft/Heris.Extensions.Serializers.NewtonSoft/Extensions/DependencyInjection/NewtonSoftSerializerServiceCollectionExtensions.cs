namespace Heris.Extensions.DependencyInjection;

public static class NewtonSoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddHerisNewtonSoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, NewtonSoftSerializer>();
}