using KibernumCrud.DataAccess.Entities;

namespace KibernumCrud.IntegrationTests.Configuration;

public class DefaultTestEntities
{
    public User User { get; protected set; } = default!;


    public static DefaultTestEntities Create()
    {
        var dte = new DefaultTestEntities();
        User user = TestHelper.UserFaker.Generate();
        Contact contact = TestHelper.ContactFaker.Generate();
        
        dte.User = user;

        return dte;
    }
}