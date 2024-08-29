using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Handlers.V1.Users.Queries.GetUser;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Requests.Users;
using KibernumCrud.Application.Models.V1.Responses.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KibernumCrud.Api.Controllers.V1;

public class UsersController : BaseController
{
    //[AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCreateCommand();
        
        var result = await Sender.Send(command, cancellationToken);

        return result.Match(user => CreatedAtAction(nameof(CreateUser), user),
            error => Problem(error.Message));
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(userId);
        
        var result = await Sender.Send(query, cancellationToken);
        
        return result.Match(user => Ok(user),
            error => Problem(error.Message));
    }

}