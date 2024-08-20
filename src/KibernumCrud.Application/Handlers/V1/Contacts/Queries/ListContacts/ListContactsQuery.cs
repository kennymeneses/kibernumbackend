using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Requests.Commons;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Queries.ListContacts;

public record ListContactsQuery : PaginatedQuery , IRequest<EitherResult<PaginatedContacts, Exception>>;