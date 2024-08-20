using Microsoft.AspNetCore.Mvc;

namespace KibernumCrud.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
}