using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using KibernumCrud.Application.Models.V1.Requests.Contacts;
using KibernumCrud.Application.Models.V1.Requests.Users;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.Application.Models.V1.Responses.Users;
using KibernumCrud.IntegrationTests.Configuration;
using Testcontainers.PostgreSql;

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
    
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
    
    public async Task DisposeAsync()
    {
        await Task.Delay(200);
        await _resetDatabase();
    }

    [Fact]
    public async Task CreateContact_ShouldReturnCreatedContact()
    {
        DefaultTestEntities dte = DefaultTestEntities.Create();
        
        CreateUserRequest userRequest = new CreateUserRequest(TestHelper.Faker.Person.FirstName,
            TestHelper.Faker.Person.LastName,
            TestHelper.Faker.Person.Email,"");
        
        UserDto userDtoCreated = await _client.As(dte.User).Create<CreateUserRequest, UserDto>("api/v1/Users", userRequest);
        userDtoCreated.LastName.Should().Be(userRequest.Lastname);
    }
    
    [Fact]
    public async Task GetListContactsSuccessfully_WhenRequestIsValid()
    {
        DefaultTestEntities dte = DefaultTestEntities.Create();
        
        CreateUserRequest userRequest = new CreateUserRequest(TestHelper.Faker.Person.LastName,
            TestHelper.Faker.Person.LastName,
            TestHelper.Faker.Person.Email,
        TestHelper.Faker.Person.Email);

        UserDto userCreated =
            await _client.As(dte.User).Create<CreateUserRequest, UserDto>("api/v1/Users", userRequest);
        
        CreateContactRequest request = new CreateContactRequest(
            userCreated.Uuid,
            TestHelper.Faker.Person.FirstName,
            TestHelper.Faker.Person.Phone);
        
        ContactDto contactCreated = await _client.As(dte.User).Create<CreateContactRequest, ContactDto>("api/v1/Contacts", request);
        contactCreated.Id.Should().NotBeEmpty();
        
        HttpResponseMessage contact = await _client.As(dte.User).GetAsync($"api/v1/Contacts/{contactCreated.Id}");
        contact.StatusCode.Should().Be(HttpStatusCode.OK);
        ContactDto contactFound = await contact.Read<ContactDto>();
        contactFound.Id.Should().NotBeEmpty();
    }
}