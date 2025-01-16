using Application.DI;
using Infrastructure.Tokens.Getter;
using Infrastructure.Tokens.JWT;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Persistense.DI;
using WebAPI.Configurations;
using WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

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

var configuration = builder.Configuration.GetSection("ServicesConfigs").Get<ServiceConfiguration>();

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddApplication()
    .AddHttpTokenGetter("AuthToken", "RefreshToken")
    .AddPersistense(
    ex => ex.UseNpgsql(configuration.MainDatabase.ConnectionString).EnableDetailedErrors()
    )
    .AddJWTTokenDriver("mySecret");

var app = builder.Build();

app.UseRouting();
app.MapGrpcService<GetInfoController>().RequireHost($"*:{grpcPort}");
app.MapControllers().RequireHost($"*:{restPort}");

app.Run();
