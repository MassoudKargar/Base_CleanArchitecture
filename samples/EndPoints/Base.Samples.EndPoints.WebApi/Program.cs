using Base.Infrastructure.Ef.PostgreSQL;
using Base.Sample.Application.People.Validators;
using Base.Sample.Application.People.ViewModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBaseApiCore("Base");
builder.Services.AddValidators(typeof(PersonInsertViewModelValidator).Assembly, typeof(PersonInsertViewModel).Assembly);
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddHostedService<LocationConsumerService>();

builder.Services.AddBaseNewtonSoftSerializer();
builder.Services.AddBaseAutoMapperProfiles(option =>
{
    option.AssemblyNamesForLoadProfiles = builder.Configuration["AutoMapper:AssmblyNamesForLoadProfiles"];
});

builder.Services.ConfigurePostgreSql(builder.Configuration.GetConnectionString("BaseConnectionString"));

builder.Services.AddSwagger(builder.Configuration, "Swagger");
var app = builder.Build();

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
