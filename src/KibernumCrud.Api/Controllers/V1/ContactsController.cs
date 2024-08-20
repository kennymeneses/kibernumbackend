using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace KibernumCrud.Api.Controllers.V1;

public class ContactsController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public IActionResult GetCalculation(CancellationToken cancellationToken)
    {
        return Ok(1);
    }
}