using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Responses.Users;
using KibernumCrud.DataAccess.Configuration.Interfaces;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;

namespace KibernumCrud.Application.Handlers.V1.Users.Commands.CreateUser;

public sealed class CreateUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, EitherResult<UserDto, Exception>>
{
    public async ValueTask<EitherResult<UserDto, Exception>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FirstOrDefaultAsync<User>(x => x.Email == request.Email, cancellationToken);
        
        if(user is not null) return new InvalidOperationException();

        UserPassword userPassword = new UserPassword { Uuid = Guid.NewGuid(), Password = request.Password.HashString(), Deleted = false };
        
        User newUser = new User{Name = request.Name, Email = request.Email, Deleted = false, LastName = request.Lastname, Uuid = Guid.NewGuid(), UserPassword = userPassword};
        
        await userRepository.CreateAsync(newUser);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        User userCreated = (await userRepository.FirstOrDefaultAsync<User>(x => x.Email == request.Email, cancellationToken))!;
        
        return userCreated!.ToDto();
    }
}