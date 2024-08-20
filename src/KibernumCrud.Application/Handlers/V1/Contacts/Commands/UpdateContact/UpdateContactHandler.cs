using KibernumCrud.Application.Configuration;
using KibernumCrud.DataAccess.Entities;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.UpdateContact;

public sealed class UpdateContactHandler : IRequestHandler<UpdateContactCommand, EitherResult<Contact, Exception>>
{
    public async ValueTask<EitherResult<Contact, Exception>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}