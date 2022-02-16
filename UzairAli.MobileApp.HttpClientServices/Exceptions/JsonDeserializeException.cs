using System;

namespace UzairAli.HttpClientServices.Exceptions;
internal class JsonDeserializeException : Exception
{
    public Type ServiceType { get; }
    public JsonDeserializeException(Type serviceType)
    {
        ServiceType = serviceType;
    }

    public JsonDeserializeException(string message, Type serviceType) : base(message)
    {
        ServiceType = serviceType;
    }

}
