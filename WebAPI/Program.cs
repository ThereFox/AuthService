

using Application.DI;
using Infrastructure.Tokens.JWT;
using Microsoft.EntityFrameworkCore;
using Persistense.DI;
using WebAPI.Configurations;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.GetSection("ServicesConfigs").Get<ServiceConfiguration>();

builder.Services.AddGrpc();

builder.Services
    .AddApplication()
    .AddPersistense(
    ex => ex.UseNpgsql(configuration.MainDatabase.ConnectionString)
    )
    .AddJWTTokenDriver();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GetInfoController>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
