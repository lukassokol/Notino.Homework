using Notino.Homework.Requests;

namespace Notino.Homework.Extensions;

public static class PathExtensions
{
    public static FileType GetExtension(this string fileName)
    {
        var extension = Path.GetExtension(fileName).Trim('.').ToLower();

        return (FileType)Enum.Parse(typeof(FileType), extension);
    }

    public static string GetTargetFileName(this string fileName, FileType targetType)
    {
        return fileName.Replace(Path.GetExtension(fileName), $".{targetType}");
    }
}
