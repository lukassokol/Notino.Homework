using MediatR;
using Notino.Homework.Dtos;
using Notino.Homework.Providers.TotalCommander;
using System.ComponentModel.DataAnnotations;

namespace Notino.Homework.Requests;

public class DownloadFileFromUrl
{
    public class Request : IRequest<FileResponse>
    {
        [Required]
        public Uri Url { get; set; }
    }

    public class Handler : IRequestHandler<Request, FileResponse>
    {
        private readonly IFileManager _fileManager;

        public Handler(IFileManager fileManagere)
        {
            _fileManager = fileManagere;
        }

        public async Task<FileResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var fileBytes = await _fileManager.GetFileFromUrl(request.Url, cancellationToken);

            return new FileResponse()
            {
                Bytes = fileBytes,
                ContentType = "image/jpeg",
                Name = Path.GetFileName(request.Url.LocalPath)
            };
        }
    }

}
