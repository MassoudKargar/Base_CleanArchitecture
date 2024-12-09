var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBaseApiCore("Heris");
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddHostedService<LocationConsumerService>();

builder.Services.AddHerisNewtonSoftSerializer();
builder.Services.AddHerisAutoMapperProfiles(option =>
{
    option.AssemblyNamesForLoadProfiles = builder.Configuration["AutoMapper:AssmblyNamesForLoadProfiles"];
});

builder.Services.AddDbContext<BaseDbContext, SampleDbContext>(
    c => c.UseNpgsql(builder.Configuration.GetConnectionString("HerisConnectionString"), options =>
    {
        options.MigrationsAssembly(typeof(SampleDbContext).Assembly.GetName().Name);
    }));

builder.Services.AddSwagger(builder.Configuration, "Swagger");
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
    if (!app.Environment.IsDevelopment())
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
