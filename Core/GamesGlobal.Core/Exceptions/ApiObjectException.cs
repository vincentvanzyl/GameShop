using System.Net;
using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Exceptions;

public class ApiObjectException : Exception
{
    public ApiObjectException(object value, HttpStatusCode statusCode) : base(statusCode.ToString())
    {
        Value = value;
        StatusCode = statusCode;
    }

    public ApiObjectException(string errorMessage, HttpStatusCode statusCode) : base(errorMessage)
    {
        Value = ErrorResult.Wrap(errorMessage, null);
        StatusCode = statusCode;
    } 

    public ApiObjectException(HttpStatusCode statusCode) : base(statusCode.ToString())
    {
        Value = new {};
        StatusCode = statusCode;
    }
  

    public object Value { get; }

    public HttpStatusCode StatusCode { get; }

    public bool IgnoreFailedIpIncrement { get; set; }
}