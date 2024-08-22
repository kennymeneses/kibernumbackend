using System.Net;
using FluentAssertions;
using KibernumCrud.IntegrationTests.Configuration;

namespace KibernumCrud.IntegrationTests;

[Collection(nameof(IntegrationTestsKibernumCollectionFixture))]
public class ContactTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;
    private readonly KibernumApiFactory _kibernumApiFactory;

    public ContactTests(KibernumApiFactory factory)
    {
        _kibernumApiFactory = factory;
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
    }
    
    public async Task InitializeAsync()
    {
        await _resetDatabase();
    }
    
    public async Task DisposeAsync()
    {
        await Task.Delay(200);
        await _resetDatabase();
    }
}