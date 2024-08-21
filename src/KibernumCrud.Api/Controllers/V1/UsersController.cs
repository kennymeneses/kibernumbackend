using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Requests.Users;
using KibernumCrud.Application.Models.V1.Responses.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KibernumCrud.Api.Controllers.V1;

public class UsersController : BaseController
{
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCreateCommand();
        
        EitherResult<UserDto, Exception> result = await Sender.Send(command, cancellationToken);

        return result.Match(user => Ok(user),
            error => Problem(error.Message));
    }

}