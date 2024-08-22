namespace KibernumCrud.Application.Models.V1.Requests.Contacts;

public sealed record CreateContactRequest(Guid UserId, string Name, string PhoneNumber);