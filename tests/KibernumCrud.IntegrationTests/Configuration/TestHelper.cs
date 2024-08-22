using Bogus;
using KibernumCrud.DataAccess.Entities;

namespace KibernumCrud.IntegrationTests.Configuration;

public static class TestHelper
{
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
}