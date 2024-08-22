using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Queries.ListContacts;

public sealed class ListContactsHandler(
    IUserRepository userRepository,
    IContactRepository contactRepository
    )
    : IRequestHandler<ListContactsQuery, EitherResult<PaginatedDtoContactsResult, Exception>>
{
    public async ValueTask<EitherResult<PaginatedDtoContactsResult, Exception>> Handle(ListContactsQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FirstOrDefaultAsync<User>(user => user.Uuid == request.UserId, cancellationToken);
        
        if(user is null)  return new KeyNotFoundException();

        var contacts = await contactRepository.ListAsync(cancellationToken);
        
        var totalItems = await contactRepository.CountAsync(cancellationToken);

        var result = new PaginatedContactsResult(request.SafePage, request.SafePageSize, totalItems, contacts);

        return result.ToPaginateResult();
    }
}