using KibernumCrud.Application.Configuration;
using KibernumCrud.DataAccess.Entities;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.CreateContact;

public sealed record CreateContactCommand : IRequest<EitherResult<Contact, Exception>>;