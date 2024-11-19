namespace Base.Samples.EndPoints.WebApi.Extensions;

public static class HostingExtensions
{
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

