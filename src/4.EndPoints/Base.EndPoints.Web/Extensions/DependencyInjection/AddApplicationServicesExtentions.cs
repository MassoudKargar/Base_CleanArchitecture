namespace Base.EndPoints.Web.Extensions.DependencyInjection;

public static class AddApplicationServicesExtensions
{
    public static IServiceCollection AddBaseApplicationServices(this IServiceCollection services,
                                                                 IEnumerable<Assembly> assembliesForSearch)
        => services
                   .AddFluentValidators(assembliesForSearch);

    //private static IServiceCollection AddDispatcherDecorators(this IServiceCollection services)
    //{
    //    services.AddTransient<CommandDispatcher, CommandDispatcher>();
    //    services.AddTransient<CommandDispatcherDecorator, CommandDispatcherDomainExceptionHandlerDecorator>();
    //    services.AddTransient<CommandDispatcherDecorator, CommandDispatcherValidationDecorator>();

    //    services.AddTransient<ICommandDispatcher>(c =>
    //    {
    //        var commandDispatcher = c.GetRequiredService<CommandDispatcher>();
    //        var decorators = c.GetServices<CommandDispatcherDecorator>().ToList();
    //        if (decorators?.Any() == true)
    //        {
    //            decorators = decorators.OrderBy(c => c.Order).ToList();
    //            var listFinalIndex = decorators.Count - 1;
    //            for (int i = 0; i <= listFinalIndex; i++)
    //            {
    //                if (i == listFinalIndex)
    //                {
    //                    decorators[i].SetCommandDispatcher(commandDispatcher);

    //                }
    //                else
    //                {
    //                    decorators[i].SetCommandDispatcher(decorators[i + 1]);
    //                }
    //            }
    //            return decorators[0];
    //        }
    //        return commandDispatcher;
    //    });
    //    return services;
    //}

    private static IServiceCollection AddFluentValidators(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddValidatorsFromAssemblies(assembliesForSearch);
}

