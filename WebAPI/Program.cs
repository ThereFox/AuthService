using Application.DI;
using Infrastructure.Tokens.Getter;
using Infrastructure.Tokens.JWT;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistense.DI;
using WebAPI.Configurations;
using WebAPI.Configurations.Services;
using WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

var secretsFile = Environment.GetEnvironmentVariable("Configuration");

if (string.IsNullOrEmpty(secretsFile) == false)
{
    builder.Configuration.AddJsonFile(secretsFile);   
}

var restPort = int.Parse(Environment.GetEnvironmentVariable("REST_PORT") ?? "5279");
var grpcPort = int.Parse(Environment.GetEnvironmentVariable("gRPC_PORT") ?? "6000");

builder.WebHost.ConfigureKestrel(
    kestrel =>
    {
        kestrel.ListenAnyIP(restPort, options =>
        {
            options.Protocols = HttpProtocols.Http1;
        });
        kestrel.ListenAnyIP(grpcPort, options =>
        {
            options.Protocols = HttpProtocols.Http2;
        });
    });

var configuration = builder.Configuration.GetSection("ServicesConfigs");

var servicesConfig = configuration.Get<ServiceConfiguration>();

if (servicesConfig == null)
{
    servicesConfig = JsonConvert.DeserializeObject<ServiceConfiguration>(configuration.Value);

    if (servicesConfig == null)
    {
        throw new ApplicationException("ServicesConfigs not found");
    }
}

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddGrpcReflection();

builder.Services
    .AddApplication()
    .AddHttpTokenGetter(servicesConfig.JWTConfig.HeaderName, servicesConfig.JWTConfig.CookeyName)
    .AddPersistense(
    ex => ex.UseNpgsql(servicesConfig.MainDatabase.ConnectionString).EnableDetailedErrors()
    )
    .AddJWTTokenDriver(servicesConfig.JWTConfig.Secret);

var app = builder.Build();

app.UseRouting();
app.MapGrpcService<GetInfoController>().RequireHost($"*:{grpcPort}");
app.MapGrpcReflectionService();
app.MapControllers().RequireHost($"*:{restPort}");


app.Run();


public partial class Program { }