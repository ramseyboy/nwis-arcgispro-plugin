using System.Runtime.Serialization;

namespace NwisApiClient.Exceptions;

[Serializable]
public class NwisParameterException: ArgumentException
{
    public NwisParameterException()
    {
    }

    protected NwisParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NwisParameterException(string? message) : base(message)
    {
    }

    public NwisParameterException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public NwisParameterException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public NwisParameterException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}