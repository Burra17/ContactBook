using ContactBook.Application.Contacts;
using ContactBook.Application.Contacts.Commands.CreateContact;
using ContactBook.Application.Contacts.Queries.GetContactById;
using ContactBook.Application.Contacts.Queries.GetContacts;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.API.Controllers;

public class ContactsController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts()
    {
        var result = await Mediator.Send(new GetContactsQuery());
        return HandleResult(result);
    }

    [HttpGet("{contactId:guid}")]
    public async Task<IActionResult> GetContactById(Guid contactId)
    {
        var result = await Mediator.Send(new GetContactByIdQuery(contactId));
        return HandleResult(result);
    }
}