using ContactBook.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected ActionResult HandleResult<T>(OperationResult<T> result)
    {
        if (result == null) return NotFound();

        if (result.IsSuccess && result.Data != null)
            return Ok(result.Data);

        if (result.IsSuccess && result.Data == null)
            return NotFound();

        return BadRequest(result.ErrorMessage);
    }

    protected ActionResult HandleResult(OperationResult result)
    {
        if (result == null) return NotFound();

        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.ErrorMessage);
    }
}