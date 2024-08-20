using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace KibernumCrud.Api.Controllers.V2;

[ApiVersion("2.0")]
public class ContactsController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public IActionResult GetCalculation(CancellationToken cancellationToken)
    {
        return Ok(2);
    }
}