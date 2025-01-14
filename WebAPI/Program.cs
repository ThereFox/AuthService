using Application.DI;
using Infrastructure.Tokens.Getter;
using Infrastructure.Tokens.JWT;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Persistense.DI;
using WebAPI.Configurations;
using WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(
    kestrel =>
    {
        kestrel.ListenAnyIP(5279, options =>
        {
            options.Protocols = HttpProtocols.Http1;
        });
        kestrel.ListenAnyIP(6000, options =>
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
app.MapGrpcService<GetInfoController>().RequireHost("*:6000");
app.MapControllers().RequireHost("*:5279");

app.Run();
