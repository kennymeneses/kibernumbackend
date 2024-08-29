using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Responses.Users;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Users.Queries.GetUser;

public sealed class GetUserHandler(
    IUserRepository userRepository) : IRequestHandler<GetUserQuery, EitherResult<UserDto, Exception>>
{
    public async ValueTask<EitherResult<UserDto, Exception>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FirstOrDefaultAsync<User>(user => user.Uuid == request.UserId, cancellationToken);
        
        if (user is null) return new KeyNotFoundException();

        return user.ToDto();
    }
}