using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Queries.GetContact;

public sealed class GetContactHandler(
    IContactRepository contactRepository)
    : IRequestHandler<GetContactQuery, EitherResult<ContactDto, Exception>>
{
    public async ValueTask<EitherResult<ContactDto, Exception>> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        Contact? contact = await contactRepository.FirstOrDefaultAsync<Contact>(contact => contact.Uuid == request.UserId, cancellationToken);
        
        if (contact is null) return new KeyNotFoundException();

        return contact.ToDto();
    }
}