using MediatR;
using Notino.Homework.Converters;
using Notino.Homework.Dtos;
using Notino.Homework.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Notino.Homework.Requests;

public class DocumentConverter
{
    public class Request : IRequest<FileResponse>
    {
        [Required]
        public IFormFile SourceFile { get; set; }
        [Required]
        public FileType OutputType { get; set; }
    }

    public class Handler : IRequestHandler<Request, FileResponse>
    {
        private readonly IConversionProvider _conversionProvider;

        public Handler(IConversionProvider conversionProvider)
        {
            _conversionProvider = conversionProvider;
        }

        public async Task<FileResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var document = await request.SourceFile.ConvertToStringAsync(cancellationToken);
            var fileName = request.SourceFile.FileName;

            var sourceType = fileName.GetExtension();
            var converter = _conversionProvider.GetConverter(sourceType, request.OutputType);

            var targetDocument = converter.Convert(document);

            return new FileResponse()
            {
                Bytes = Encoding.ASCII.GetBytes(targetDocument),
                Name = fileName.GetTargetFileName(request.OutputType),
                ContentType = $"text/{request.OutputType}"
            };
        }
    }
}

public enum FileType
{
    xml,
    json
}
