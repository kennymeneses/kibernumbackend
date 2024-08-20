using KibernumCrud.Application.Configuration;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.DeleteContact;

public sealed class DeleteContactHandler : IRequestHandler<DeleteContactCommand, EitherResult<Guid, Exception>> 
{
    public async ValueTask<EitherResult<Guid, Exception>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}