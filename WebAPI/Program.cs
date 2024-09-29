

using Application.DI;
using Infrastructure.Tokens.JWT;
using Persistense.DI;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services
    .AddApplication()
    .AddPersistense()
    .AddJWTTokenDriver();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GetInfoController>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
