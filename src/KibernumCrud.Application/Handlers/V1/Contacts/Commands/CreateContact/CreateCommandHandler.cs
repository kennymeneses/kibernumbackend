using System.Runtime.InteropServices.JavaScript;
using KibernumCrud.Application.Configuration;
using KibernumCrud.DataAccess.Entities;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.CreateContact;

public sealed class CreateCommandHandler : IRequestHandler<CreateContactCommand, EitherResult<Contact, Exception>>
{
    public async ValueTask<EitherResult<Contact, Exception>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}