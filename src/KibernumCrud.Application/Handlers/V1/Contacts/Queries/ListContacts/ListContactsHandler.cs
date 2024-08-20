using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Queries.ListContacts;

public sealed class ListContactsHandler : IRequestHandler<ListContactsQuery, EitherResult<PaginatedContacts, Exception>>
{
    public ValueTask<EitherResult<PaginatedContacts, Exception>> Handle(ListContactsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}