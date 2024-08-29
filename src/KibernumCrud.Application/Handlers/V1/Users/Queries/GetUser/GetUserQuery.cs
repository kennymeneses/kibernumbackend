using System.Runtime.InteropServices.JavaScript;
using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Responses.Users;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Users.Queries.GetUser;

public sealed record GetUserQuery(Guid UserId) : IRequest<EitherResult<UserDto, Exception>>;