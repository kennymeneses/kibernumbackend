using KibernumCrud.Application.Configuration;
using KibernumCrud.DataAccess.Entities;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Queries.GetContact;

public sealed record GetContactQuery : IRequest<EitherResult<Contact, Exception>>;