using System.Net;

namespace Cafe.Domain.Exceptions;

public class ApiException : Exception
{
    public ApiException(HttpStatusCode statusCode, string message, object data = null) : base(message)
    {
        StatusCode = statusCode;
        Data = data;
    }

    public HttpStatusCode StatusCode { get; set; }
    public object Data { get; private set; }
}