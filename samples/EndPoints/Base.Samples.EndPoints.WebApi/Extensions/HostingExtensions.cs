namespace Base.Samples.EndPoints.WebApi.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;

        builder.Services.AddBaseApiCore("Base");
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddBaseNewtonSoftSerializer();
        builder.Services.AddBaseAutoMapperProfiles(option =>
        {
            option.AssemblyNamesForLoadProfiles = "Base";
        });

        builder.Services.AddDbContext<BaseDbContext, SampleDbContext>(
            c => c.UseSqlServer(configuration.GetConnectionString("BaseConnectionString"), options =>
            {
                options.MigrationsAssembly(typeof(SampleDbContext).Assembly.GetName().Name);
            }));

        builder.Services.AddSwagger(configuration, "Swagger");

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCustomExceptionHandler();
        app.UseSwaggerUI("Swagger");
        app.UseStatusCodePages();
        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });

        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
    public static void AddRateLimitSetting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            RateLimitPartition.GetSlidingWindowLimiter(
                partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ??
                httpContext.User.Identity?.Name ??
                httpContext.Request.Headers.Host.ToString(),
                factory: partition => new SlidingWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 100,
                    QueueLimit = 100,
                    Window = TimeSpan.FromMinutes(1),
                    SegmentsPerWindow = 40
                }));

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

