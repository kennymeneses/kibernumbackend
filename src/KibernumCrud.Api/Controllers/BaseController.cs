using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace KibernumCrud.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
    private ISender? _sender;
    
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}