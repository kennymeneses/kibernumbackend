using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.DataAccess.Configuration.Interfaces;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.UpdateContact;

public sealed class UpdateContactHandler(
    IContactRepository contactRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateContactCommand, EitherResult<ContactDto, Exception>>
{
    public async ValueTask<EitherResult<ContactDto, Exception>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        Contact? contact = await contactRepository.FirstOrDefaultAsync<Contact>(contact => contact.Uuid == request.ContactId, cancellationToken);
        
        if (contact is null) return new KeyNotFoundException();
        
        contact.Name = request.Name;
        contact.PhoneNumber = request.PhoneNumber;
        contact.CreationDate = DateTime.SpecifyKind(contact.CreationDate, DateTimeKind.Utc);
        contact.Deleted = false;
        
        contactRepository.Update(contact);
        await unitOfWork.CommitChangesAsync(cancellationToken);
        
        return contact.ToDto();
    }
}