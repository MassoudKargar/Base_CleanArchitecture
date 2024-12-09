using Base.Extensions.DependencyInjection;
using Heris.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddBaseApiAuthentication(builder.Configuration, "ApiAuthentication");
builder.Services.AddHerisSwagger(builder.Configuration, "Swagger");

var app = builder.Build();

app.UseHerisSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();