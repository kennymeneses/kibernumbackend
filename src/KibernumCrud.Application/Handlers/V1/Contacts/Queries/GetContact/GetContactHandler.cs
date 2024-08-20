using KibernumCrud.Application.Configuration;
using KibernumCrud.DataAccess.Entities;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Queries.GetContact;

public sealed class GetContactHandler : IRequestHandler<GetContactQuery, EitherResult<Contact, Exception>>
{
    public async ValueTask<EitherResult<Contact, Exception>> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}