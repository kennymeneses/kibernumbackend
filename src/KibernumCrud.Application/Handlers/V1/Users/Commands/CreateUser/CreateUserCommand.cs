using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Responses.Users;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Name, string Lastname, string Email, string Password) : IRequest<EitherResult<UserDto, Exception>>;