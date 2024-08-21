using KibernumCrud.Application.Handlers.V1.Auth.Commands.Login;
using KibernumCrud.Application.Models.V1.Requests.Login;
using KibernumCrud.Application.Models.V1.Responses.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KibernumCrud.Api.Controllers.V1;

public class AuthController : BaseController
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        
        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            login => Ok(login),
            error => Problem(error.Message)
        );
    }
}