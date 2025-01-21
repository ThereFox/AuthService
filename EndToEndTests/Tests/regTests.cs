using System.Net.Http.Json;
using Application.InputObjects;
using EndToEndTests.Fixture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Testcontainers.PostgreSql;
using WebAPI;
using WebAPI.Configurations;
using WebAPI.Configurations.Services;
using WebAPI.InputObjects;

namespace EndToEndTests;

public class regTests : IClassFixture<DatabaseFixture>, IAsyncLifetime
{
    public const string HeaderName = "Authorization";
    public const string CookeyName = "Refresh";
    
    private WebApplicationFactory<Program> _sut;
    private readonly DatabaseFixture _databaseFixture;
    
    public regTests(DatabaseFixture fixture)
    {
        _databaseFixture = fixture;
    }
    
    [Fact]
    public async Task SucsessfullRegistration()
    {
        //arrange
        var client = _sut.CreateClient();
        var regInfo = new RegistrationInfoInputObject()
        {
            Login = "testLogin",
            Password = "testPassword",
        };
        var registrationContent = JsonContent.Create(regInfo);
        
        //act
        var registrateResponseMessage = await client.PostAsync(
            "http://localhost:5501/public/reg", registrationContent);
        
        //assert
        Assert.True(registrateResponseMessage.IsSuccessStatusCode);
        Assert.True(registrateResponseMessage.Headers.Contains(HeaderName));
        Assert.True(registrateResponseMessage.Headers.Contains("Set-Cookie"));
        Assert.True(registrateResponseMessage.Headers.GetValues("Set-Cookie").First().StartsWith(CookeyName));
    }

    [Fact]
    public async Task CanAuthoriseRegistration()
    {
        //arrange
        var client = _sut.CreateClient();
        var regInfo = new RegistrationInfoInputObject()
        {
            Login = "testLogin",
            Password = "testPassword",
        };
        var loginInfo = new LogInInfoInputObject()
        {
            Login = "testLogin",
            Password = "testPassword",
        };
        var registrationContent = JsonContent.Create(regInfo);
        var loginContent = JsonContent.Create(loginInfo);
        
        //act
        var registrateResponseMessage = await client.PostAsync(
            "http://localhost:5501/public/reg", registrationContent);
        
        //assert
        Assert.True(registrateResponseMessage.IsSuccessStatusCode);
        Assert.True(registrateResponseMessage.Headers.Contains(HeaderName));
        Assert.True(registrateResponseMessage.Headers.Contains("Set-Cookie"));
        Assert.True(registrateResponseMessage.Headers.GetValues("Set-Cookie").First().StartsWith(CookeyName));
        
        //act
        var authResponseMessage = await client.PostAsync(
            "http://localhost:5501/public/login", loginContent);
        
        //assert
        Assert.True(authResponseMessage.IsSuccessStatusCode);
    }

    
    public async Task InitializeAsync()
    {
        await _databaseFixture.InitializeAsync();
        
        var app = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(
                ex =>
                {
                    var config = JsonConvert.SerializeObject(
                        new ServiceConfiguration(
                            new DatabaseConfigInputObject(_databaseFixture.ConnectionString),
                            new JWTConfig(HeaderName, CookeyName, "secret")
                        ));
                    ex.UseSetting("ServicesConfigs", config);

                    ex.ConfigureAppConfiguration(
                        configuration => configuration.AddInMemoryCollection(
                            [new KeyValuePair<string, string?>("ServicesConfigs", config)]
                            )
                        );
                    
                    Environment.SetEnvironmentVariable("REST_PORT", "5501");
                    Environment.SetEnvironmentVariable("gRPC_PORT", "6001");

                    ex.ConfigureAppConfiguration(ex => ex.Build());
                });
        _sut = app;
    }

    public async Task DisposeAsync()
    {
        await _databaseFixture.DisposeAsync();
    }
}