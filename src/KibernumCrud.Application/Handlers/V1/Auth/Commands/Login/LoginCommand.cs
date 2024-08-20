using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Responses.Login;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Auth.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<EitherResult<LoginResult, Exception>>;