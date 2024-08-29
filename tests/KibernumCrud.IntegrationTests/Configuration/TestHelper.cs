using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;
using DateOnlyTimeOnly.AspNet.Converters;
using FluentAssertions;
using KibernumCrud.Application.Models.V1.Security;
using KibernumCrud.DataAccess.Entities;

namespace KibernumCrud.IntegrationTests.Configuration;

public static class TestHelper
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        Converters =
        {
            new JsonStringEnumConverter(), new DateOnlyJsonConverter(), new TimeOnlyJsonConverter()
        }
    };
    
    public static readonly Faker Faker = new();
    public static Faker<Contact> ContactFaker => new();

    public static readonly Faker<User> UserFaker = new Faker<User>().CustomInstantiator(
        faker =>
        {
            var contacts = new List<Contact> { ContactFaker };
            
            return new User
            {
                Id = faker.Random.Number(),
                Email = faker.Internet.Email(),
                LastName = faker.Person.LastName,
                Name = faker.Person.FullName,
                CreationDate = faker.Date.Past(),
                Deleted = false,
                Contacts = contacts
            };
        });

    public static async Task<TType> Read<TType>(this HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<TType>(Options) ?? throw new InvalidOperationException();
    }
    
    public static async Task<TResponse> Create<TRequest, TResponse>(
        this HttpClient client,
        string uri,
        TRequest request,
        HttpStatusCode statusCode = HttpStatusCode.Created)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync(uri, request, Options);
        httpResponseMessage.StatusCode.Should().Be(statusCode);
        TResponse response = await httpResponseMessage.Read<TResponse>();
        await Task.Delay(200);
        return response;
    }
    
    public static HttpClient As(this HttpClient httpClient, User user)
    {
        string username = user.Email;
        string[] roles = new[] { "User" };
        
        httpClient.SetFakeBearerToken(username, roles);
        return httpClient;
    }
}