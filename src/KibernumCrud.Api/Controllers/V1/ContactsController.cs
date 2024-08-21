using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Handlers.V1.Contacts.Commands.CreateContact;
using KibernumCrud.Application.Handlers.V1.Contacts.Queries.GetContact;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Requests.Contacts;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KibernumCrud.Api.Controllers.V1;

public class ContactsController : BaseController
{
    [HttpGet("{contactId:guid}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetContact([FromRoute] Guid contactId, CancellationToken cancellationToken)
    {
        var query = new GetContactQuery(contactId);
        var result = await Sender.Send(query, cancellationToken);

        return result.Match(contact => Ok(contact), error => Problem(error.Message));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetContactsList([FromQuery] ListContactsRequest request, CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var result = await Sender.Send(query, cancellationToken);
        
        return result.Match(contacts => Ok(contacts), error => Problem(error.Message));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateContactCommand(request.UserId, request.Name, request.PhoneNumber);
        var result = await Sender.Send(command, cancellationToken);
        
        return result.Match(contact => Ok(contact), error => Problem(error.Message));
    }
}