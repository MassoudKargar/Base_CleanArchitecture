using Base.Extensions.BackgroundWorker.Abstractions;
using Base.Extensions.BackgroundWorker.KafkaConsumer;
using Base.Infra.Validator;
using Base.Sample.Application.KafkaServices;
using Base.Sample.Application.People.Validators;
using Base.Sample.BackgroundWorker.LocationService;

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

builder.Services.InitializeValidators();
builder.Services.RegisterValidatorsByAssembly(typeof(PersonInsertViewModelValidator).Assembly, typeof(PersonInsertViewModel)?.Namespace ?? "");


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
