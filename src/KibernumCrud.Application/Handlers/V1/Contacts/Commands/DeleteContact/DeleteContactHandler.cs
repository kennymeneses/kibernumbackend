using KibernumCrud.Application.Configuration;
using KibernumCrud.DataAccess.Configuration.Interfaces;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.DeleteContact;

public sealed class DeleteContactHandler(
    IContactRepository contactRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteContactCommand, EitherResult<Guid, Exception>> 
{
    public async ValueTask<EitherResult<Guid, Exception>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        Contact? contact = await contactRepository.FirstOrDefaultAsync<Contact>(contact => contact.Uuid == request.ContactId, cancellationToken);
        
        if (contact is null) return new KeyNotFoundException();
        
        contact.CreationDate = DateTime.SpecifyKind(contact.CreationDate, DateTimeKind.Utc);
        contact.Deleted = true;
        
        contactRepository.Update(contact);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return request.ContactId;
    }
}