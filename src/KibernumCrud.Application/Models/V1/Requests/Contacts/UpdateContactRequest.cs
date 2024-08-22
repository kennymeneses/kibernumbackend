namespace KibernumCrud.Application.Models.V1.Requests.Contacts;

public sealed record UpdateContactRequest(Guid ContactId, string Name, string PhoneNumber);