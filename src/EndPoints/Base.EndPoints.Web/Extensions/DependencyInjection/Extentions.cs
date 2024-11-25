﻿using System.Threading.RateLimiting;

namespace Base.EndPoints.Web.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddBaseDependencies(this IServiceCollection services,
        params string[] assemblyNamesForSearch)
    {
        var assemblies = GetAssemblies(assemblyNamesForSearch);

        services.AddBaseDataAccess(assemblies)
            .AddBaseUtilityServices()
            .AddCustomDependencies(assemblies)
            .AddArdalis()
            .AddMediatR(assemblies);
        services.AddBaseAutoMapperProfiles(option =>
        {
            option.AssemblyNamesForLoadProfiles = "Base";
        });
        services.AddRateLimitSetting();
        return services;
    }

    public static IServiceCollection AddBaseUtilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<BaseServices>();
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

    public static IServiceCollection AddMediatR(this IServiceCollection services, List<Assembly> assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies.ToArray());

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddFluentValidationAutoValidation();
        return services;
    }


    public static IServiceCollection AddCustomDependencies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        return services.AddWithTransientLifetime(assemblies, typeof(ITransientLifetime))
            .AddWithScopedLifetime(assemblies, typeof(IScopeLifetime))
            .AddWithSingletonLifetime(assemblies, typeof(ISingletonLifetime));
    }

    public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        return services;
    }
    public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }

    public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
        return services;
    }


    private static List<Assembly> GetAssemblies(string[] assemblyName)
    {
        var dependencies = DependencyContext.Default.RuntimeLibraries;
        List<Assembly> list = [];
        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assemblyName))
            {
                list.Add(Assembly.Load(new AssemblyName(library.Name)));
            }
        }

        return list;
    }

    
    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assemblyName)
    {
        return assemblyName.Any(d => compilationLibrary.Name.Contains(d))
            || compilationLibrary.Dependencies.Any(d => assemblyName.Any(c => d.Name.Contains(c)));
    }
    public static void AddRateLimitSetting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            //options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            //    RateLimitPartition.GetFixedWindowLimiter(
            //        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            //        factory: partition => new FixedWindowRateLimiterOptions
            //        {
            //            AutoReplenishment = true,
            //            PermitLimit = 100,
            //            QueueLimit = 0,
            //            Window = TimeSpan.FromMinutes(1)
            //        }));

            //options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            //    RateLimitPartition.GetSlidingWindowLimiter(
            //        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ??
            //                      httpContext.User.Identity?.Name ??
            //                      httpContext.Request.Headers.Host.ToString(),
            //        factory: partition => new SlidingWindowRateLimiterOptions
            //        {
            //            AutoReplenishment = true,
            //            PermitLimit = 1_000_000,
            //            QueueLimit = 1_000_000,
            //            Window = TimeSpan.FromMinutes(1),
            //            SegmentsPerWindow = 4,
            //            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            //        }));

            options.OnRejected = (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString();
                }

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");

                return new ValueTask();
            };
        });
    }
}