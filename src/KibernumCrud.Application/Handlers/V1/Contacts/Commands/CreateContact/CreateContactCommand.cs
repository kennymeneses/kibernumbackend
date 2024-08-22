using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.DataAccess.Entities;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.CreateContact;

public sealed record CreateContactCommand(Guid UserId, string Name, string PhoneNumber) : IRequest<EitherResult<ContactDto, Exception>>;