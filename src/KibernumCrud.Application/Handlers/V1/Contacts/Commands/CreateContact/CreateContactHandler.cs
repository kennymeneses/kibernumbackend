using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.DataAccess.Configuration.Interfaces;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.CreateContact;

public sealed class CreateContactHandler(
    IUserRepository userRepository,
    IContactRepository contactRepository,
    IUnitOfWork unitOfWork
    )
    : IRequestHandler<CreateContactCommand, EitherResult<ContactDto, Exception>>
{
    public async ValueTask<EitherResult<ContactDto, Exception>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FirstOrDefaultAsync<User>(contact => contact.Uuid == request.UserId, cancellationToken);

        if (user is null) return new KeyNotFoundException("User not found");
        
        var newContact = new Contact{Uuid = Guid.NewGuid(), Name = request.Name, PhoneNumber = request.PhoneNumber, UserId = user!.Id};

        await contactRepository.CreateAsync(newContact);
        
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return newContact.ToDto();
    }
}