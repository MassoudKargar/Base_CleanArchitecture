var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBaseApiCore("Base");
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddHostedService<LocationConsumerService>();

builder.Services.AddBaseNewtonSoftSerializer();
builder.Services.AddBaseAutoMapperProfiles(option =>
{
    option.AssemblyNamesForLoadProfiles = builder.Configuration["AutoMapper:AssmblyNamesForLoadProfiles"];
});

builder.Services.AddDbContext<BaseDbContext, SampleDbContext>(
    c => c.UseSqlServer(builder.Configuration.GetConnectionString("BaseConnectionString"), options =>
    {
        options.MigrationsAssembly(typeof(SampleDbContext).Assembly.GetName().Name);
    }));

builder.Services.AddSwagger(builder.Configuration, "Swagger");
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
    if (!await context.Database.CanConnectAsync())
    {
        await context.Database.MigrateAsync();
    }
}
app.UseCustomExceptionHandler();
app.UseSwaggerUI("Swagger");
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyOrigin();
    corsPolicyBuilder.AllowAnyHeader();
    corsPolicyBuilder.AllowAnyMethod();
});
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
