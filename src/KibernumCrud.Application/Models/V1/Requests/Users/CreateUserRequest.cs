namespace KibernumCrud.Application.Models.V1.Requests.Users;

public sealed record CreateUserRequest(string Name, string Lastname, string Email, string Password);