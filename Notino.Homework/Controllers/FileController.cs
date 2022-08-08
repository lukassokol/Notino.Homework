using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notino.Homework.Dtos;
using Notino.Homework.Requests;

namespace Notino.Homework.Controllers;

// TODO: Add authorizations
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<FileInfoResponse>> UploadFileToLocalStorage([FromForm] UploadFileToLocalStorage.Request request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Created(response.Path, response);
    }

    [HttpGet("download")]
    public async Task<ActionResult> DownloadFileFromLocalStorage(DownloadFileFromLocalStorage.Request request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return File(response.Bytes, response.ContentType, response.Name);
    }

    [HttpGet("url-download")]
    public async Task<ActionResult> DownloadFileFromUrl(DownloadFileFromUrl.Request request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return File(response.Bytes, response.ContentType, response.Name);
    }
}
