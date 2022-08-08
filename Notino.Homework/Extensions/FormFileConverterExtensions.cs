using System.Text;

namespace Notino.Homework.Extensions;

public static class FormFileConverterExtensions
{
    public static async Task<string> ConvertToStringAsync(this IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));

        var buffer = new byte[(int)file.Length];

        using var memoryStream = new MemoryStream(buffer);

        await file.CopyToAsync(memoryStream, cancellationToken);
        var str = Encoding.ASCII.GetString(memoryStream.ToArray());

        return str;
    }

    public static async Task<byte[]> ConvertToBytesAsync(this IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));

        var buffer = new byte[(int)file.Length];

        using var memoryStream = new MemoryStream(buffer);
        await file.CopyToAsync(memoryStream, cancellationToken);

        return buffer;
    }

    public static async Task<byte[]> HttpResponseToBytesAsync(this HttpResponseMessage response, Uri url, CancellationToken cancellationToken)
    {
        var content = response.Content;
        using var memoryStream = new MemoryStream();
        await content.CopyToAsync(memoryStream);

        return memoryStream.ToArray();
    }
}

