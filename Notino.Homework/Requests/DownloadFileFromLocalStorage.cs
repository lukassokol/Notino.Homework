using MediatR;
using Notino.Homework.Dtos;
using Notino.Homework.Providers.TotalCommander;
using System.ComponentModel.DataAnnotations;

namespace Notino.Homework.Requests;

public class DownloadFileFromLocalStorage
{
    public class Request : IRequest<FileResponse>
    {
        [Required]
        public string Path { get; set; }
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
            var fileBytes = await _fileManager.GetFileFromLocalStorage(request.Path, cancellationToken);

            return new FileResponse()
            {
                Bytes = fileBytes,
                ContentType = "text/plain",
                Name = Path.GetFileName(request.Path)
            };
        }
    }
}
