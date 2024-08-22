using System.Data.Common;
using KibernumCrud.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
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
        
        await InitializeRespawner();
        await Task.Delay(1000);
        
        await ApiFactoryTestUtils.MigrateDatabase(Services);

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
                ApiFactoryTestUtils.UseLocalDatabase(services, _postgreSqlContainer, EnableTestDatabaseInspection);
                services.RemoveAll(typeof(IHostedService));
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
    
    private async Task InitializeRespawner()
    {
        (DbConnection dbConnectionReturned, Respawner respawnerReturned) =
            await ApiFactoryTestUtils.InitializeRespawner(_postgreSqlContainer, DefaultDbUsernamePassword);
        _dbConnection = dbConnectionReturned;
        _respawner = respawnerReturned;
    }
    
    
}