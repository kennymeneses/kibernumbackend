using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Requests.Commons;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Queries.GetContact;

public sealed record GetContactQuery(Guid UserId) : IRequest<EitherResult<ContactDto, Exception>>;