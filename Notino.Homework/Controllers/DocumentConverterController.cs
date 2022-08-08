using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notino.Homework.Requests;

namespace Notino.Homework.Controllers;

[Route("[controller]")]
public class DocumentConverterController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentConverterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("convert-file")]
    public async Task<ActionResult> ConvertFile([FromForm] DocumentConverter.Request request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return File(response.Bytes, response.ContentType, response.Name);
    }
}
