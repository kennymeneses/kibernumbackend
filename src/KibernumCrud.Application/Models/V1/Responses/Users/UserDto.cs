namespace KibernumCrud.Application.Models.V1.Responses.Users;

public sealed class UserDto
{
    public Guid Uuid { get; set; }
    public string Name { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}