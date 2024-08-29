using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Handlers.V1.Contacts.Commands.CreateContact;
using KibernumCrud.Application.Handlers.V1.Contacts.Commands.DeleteContact;
using KibernumCrud.Application.Handlers.V1.Contacts.Queries.GetContact;
using KibernumCrud.Application.Mappings;
using KibernumCrud.Application.Models.V1.Requests.Contacts;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
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
        
        return result.Match(contact => CreatedAtAction(nameof(CreateContact), contact), error => Problem(error.Message));
    }

    [HttpPut("{id:guid}", Name = nameof(UpdateContact))]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateContact([FromRoute] Guid id, [FromBody] UpdateContactRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToUpdateCommand();
        command = command with { ContactId = id };
        var result = await Sender.Send(command, cancellationToken);
        
        return result.Match(contact => Ok(contact), error => Problem(error.Message));
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var command = new DeleteContactCommand(userId);
        var result = await Sender.Send(command, cancellationToken);
        
        return result.Match(contactGuidDeleted => Ok(contactGuidDeleted), error => Problem(error.Message));
    }
}