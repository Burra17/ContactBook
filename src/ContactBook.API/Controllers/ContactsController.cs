using ContactBook.Application.Contacts;
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
}