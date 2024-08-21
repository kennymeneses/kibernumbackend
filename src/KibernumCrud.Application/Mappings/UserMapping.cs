using KibernumCrud.Application.Handlers.V1.Users.Commands.CreateUser;
using KibernumCrud.Application.Models.V1.Requests.Users;
using KibernumCrud.DataAccess.Entities;
using Riok.Mapperly.Abstractions;
using KibernumCrud.Application.Models.V1.Responses.Users;

namespace KibernumCrud.Application.Mappings;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class UserMapping
{
    public static partial UserDto ToDto(this User entity);

    public static partial CreateUserCommand ToCreateCommand(this CreateUserRequest request);
}