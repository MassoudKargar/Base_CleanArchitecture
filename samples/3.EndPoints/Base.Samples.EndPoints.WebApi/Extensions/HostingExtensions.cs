using Base.Infrastructure.SqlContext;

namespace Base.Samples.EndPoints.WebApi.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;

        builder.Services.AddBaseApiCore("Base");
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddBaseWebUserInfoService(configuration, "WebUserInfo", true);
        builder.Services.AddNonValidatingValidator();
        builder.Services.AddBaseMicrosoftSerializer();
        builder.Services.AddBaseRedisDistributedCache(option =>
        {
            option.Configuration = "localhost:6379,password=N<01!rt9Ptry-1";
            option.InstanceName = "Base.Sample.";
        });
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
        app.UseSerilogRequestLogging();
        app.UseSwaggerUI("Swagger");
        app.UseStatusCodePages();
        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });

        app.UseHttpsRedirection();

        var useIdentityServer = app.UseIdentityServer("OAuth");


        if (useIdentityServer)
        {
            app.MapControllers().RequireAuthorization();
        }
        else
        {
            app.MapControllers();
        }
        return app;
    }
}