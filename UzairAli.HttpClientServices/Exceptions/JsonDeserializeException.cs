using System;

namespace UzairAli.NetHttpClient.Exceptions;

public class JsonDeserializeException : Exception
{
    public Type ServiceType { get; }
    public JsonDeserializeException(Type serviceType)
    {
        ServiceType = serviceType;
    }

    public JsonDeserializeException(string message, Type serviceType, Exception innerException) : base(message, innerException)
    {
        ServiceType = serviceType;
    }

}
