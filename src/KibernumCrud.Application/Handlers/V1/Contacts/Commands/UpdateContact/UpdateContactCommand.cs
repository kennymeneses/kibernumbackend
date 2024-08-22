using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.UpdateContact;

public sealed record UpdateContactCommand(Guid ContactId, string Name, string PhoneNumber) : IRequest<EitherResult<ContactDto, Exception>>;