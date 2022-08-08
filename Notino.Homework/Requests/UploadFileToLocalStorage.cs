using MediatR;
using Notino.Homework.Dtos;
using Notino.Homework.Extensions;
using Notino.Homework.Providers.TotalCommander;
using System.ComponentModel.DataAnnotations;

namespace Notino.Homework.Requests;

public class UploadFileToLocalStorage
{
    public class Request : IRequest<FileInfoResponse>
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string Path { get; set; }
    }

    public class Handler : IRequestHandler<Request, FileInfoResponse>
    {
        private readonly IFileManager _fileManager;

        public Handler(IFileManager fileManagere)
        {
            _fileManager = fileManagere;
        }

        public async Task<FileInfoResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var fileBytes = await request.File.ConvertToBytesAsync(cancellationToken);
            await _fileManager.SaveFileToLocalStorage(fileBytes, request.Path, request.File.FileName, cancellationToken);

            return new FileInfoResponse()
            {
                Name = request.File.FileName,
                Path = request.Path,
                Size = fileBytes.Count()
            };
        }
    }
}
