using KibernumCrud.Application.Configuration;
using KibernumCrud.DataAccess.Entities;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.UpdateContact;

public sealed record UpdateContactCommand(int Id) : IRequest<EitherResult<Contact, Exception>>;