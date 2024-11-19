using Base.Infrastructure.SqlContext;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBaseApiCore("Base");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddNonValidatingValidator();
builder.Services.AddBaseNewtonSoftSerializer();
builder.Services.AddBaseAutoMapperProfiles(option =>
{
    option.AssemblyNamesForLoadProfiles = "Base";
});
builder.Services.AddDbContext<BaseDbContext, SampleDbContext>(
    c => c.UseSqlServer(builder.Configuration.GetConnectionString("BaseConnectionString"), options =>
    {
        options.MigrationsAssembly(typeof(SampleDbContext).Assembly.GetName().Name);
    }));
builder.Services.AddSwagger(builder.Configuration, "Swagger");

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseSwaggerUI("Swagger");
app.UseStatusCodePages();
app.UseStaticFiles();
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
app.Run();
