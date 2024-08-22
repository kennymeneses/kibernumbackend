using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Responses.Auth;
using KibernumCrud.Application.Models.V1.Security;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Auth.Commands.Login;

public sealed class LoginHandler(
    IUserRepository userRepository,
    IUserPasswordRepository userPasswordRepository,
    JwtSettings jwtSections)
    : IRequestHandler<LoginCommand, EitherResult<LoginResult, Exception>>
{
    public async ValueTask<EitherResult<LoginResult, Exception>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FirstOrDefaultAsync<User>(user => user.Email == request.Email, cancellationToken);

        if(user is null)  return new KeyNotFoundException();
        
        UserPassword? userPassword = await userPasswordRepository.FirstOrDefaultAsync<UserPassword>(userPassword => userPassword.UserId == user.Id, cancellationToken);

        if (request.Password.HashString() != userPassword!.Password) return new UnauthorizedAccessException();

        return new LoginResult{Token = LoginInnerHandler.BuildToken(user, jwtSections)};
    }
}
