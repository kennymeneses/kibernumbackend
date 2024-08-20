using System.Runtime.InteropServices.JavaScript;
using KibernumCrud.Application.Configuration;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Contacts.Commands.DeleteContact;

public sealed record DeleteContactCommand : IRequest<EitherResult<Guid, Exception>>;