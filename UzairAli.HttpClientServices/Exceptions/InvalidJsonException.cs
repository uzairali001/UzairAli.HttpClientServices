using System;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;

namespace UzairAli.HttpClient.Exceptions;
[Serializable]
public class InvalidJsonException : Exception
{
    public string Uri { get; private set; } = string.Empty;
    public string RequestString { get; } = string.Empty;
    public string ContentString { get; private set; } = string.Empty;
    public HttpStatusCode StatusCode { get; }

    public InvalidJsonException()
    {
    }

    public InvalidJsonException(string message) : base(message)
    {
    }

    public InvalidJsonException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public InvalidJsonException(string uri, string requestString, string contentString, HttpStatusCode statusCode, JsonException ex)
        : base(ex.Message, ex)
    {
        Uri = uri;
        RequestString = requestString;
        ContentString = contentString;
        StatusCode = statusCode;
    }

    protected InvalidJsonException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}