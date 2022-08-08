using Newtonsoft.Json;

namespace Notino.Homework.Middlewares.Models;

public class ErrorResponse
{
    public int StatusCode { get; private set; }
    public string Message { get; private set; }

    public ErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public override string ToString() => JsonConvert.SerializeObject(this);
}