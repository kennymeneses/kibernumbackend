using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Responses.Auth;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Auth.Commands.Logout;

public sealed record LogoutCommand(Guid UserId) : IRequest<EitherResult<LogoutResult, Exception>>;