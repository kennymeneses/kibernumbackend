using System.Data.Common;
using KibernumCrud.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Respawn;
using Testcontainers.PostgreSql;

namespace KibernumCrud.IntegrationTests.Configuration;


public class KibernumApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private const int DefaultPostgresPort = 5432;
    public HttpClient HttpClient { get; private set; }
    
    private const string DefaultDbUsernamePassword = "kibernumcrud";
    
    private const bool EnableTestDatabaseInspection = false;
    
    private DbConnection _dbConnection = default!;

    private PostgreSqlContainer _postgreSqlContainer = default!;

    private Respawner _respawner = default!;

    public async Task InitializeAsync()
    {
        _postgreSqlContainer = ApiFactoryTestUtils.CreatePostgresContainer(
            DefaultDbUsernamePassword,
            EnableTestDatabaseInspection,
            DefaultPostgresPort);

        await _postgreSqlContainer.StartAsync();
        HttpClient = CreateClient();
        await ApiFactoryTestUtils.MigrateDatabase(Services);
        await InitializeRespawner(Services);
        await Task.Delay(500);
    }
    
    public new async Task DisposeAsync()
    {
        await _dbConnection.CloseAsync();
        await _dbConnection.DisposeAsync();
        await _postgreSqlContainer.DisposeAsync();
        await base.DisposeAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(
            services =>
            {
                ApiFactoryTestUtils.MockAuthentication(services);
                ApiFactoryTestUtils.UseLocalDatabase(services, _postgreSqlContainer);
            });

        builder.ConfigureLogging(logging => logging.ClearProviders());

        builder.UseEnvironment(Constants.TestEnvName);
    }
    
    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    public KibernumApiFactory()
    {
        HttpClient = CreateClient();
    }
    
    private async Task InitializeRespawner(IServiceProvider serviceProvider)
    {
        (DbConnection dbConnectionReturned, Respawner respawnerReturned) = 
            await ApiFactoryTestUtils.InitializeRespawner(serviceProvider);
        
        _dbConnection = dbConnectionReturned;
        _respawner = respawnerReturned;
    } 
}