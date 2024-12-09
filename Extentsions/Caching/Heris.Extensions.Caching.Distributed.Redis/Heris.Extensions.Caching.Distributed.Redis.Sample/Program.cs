var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHerisNewtonSoftSerializer();
builder.Services.AddHerisRedisDistributedCache(option =>
{
    option.Configuration = "localhost:6379,password=N<01!rt9Ptry-1";
    option.InstanceName = "Heris.Sample.";
});

//Middlewares

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
