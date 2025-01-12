using Application.DI;
using Infrastructure.Tokens.Getter;
using Infrastructure.Tokens.JWT;
using Microsoft.EntityFrameworkCore;
using Persistense.DI;
using WebAPI.Configurations;
using WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.GetSection("ServicesConfigs").Get<ServiceConfiguration>();

//builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddApplication()
    .AddHttpTokenGetter("Test", "test")
    .AddPersistense(
    ex => ex.UseNpgsql(configuration.MainDatabase.ConnectionString).EnableDetailedErrors()
    )
    .AddJWTTokenDriver("mySecret");

var app = builder.Build();

app.UseRouting();
//app.MapGrpcService<GetInfoController>().RequireHost("*:6000");
app.MapControllers();

app.Run();
