namespace Base.Utility.Exceptions;
public class NullSampleException : AppException
{

    public NullSampleException()
        : base(ApiResultStatusCode.NotExtended, HttpStatusCode.NotExtended)
    {
    }

    public NullSampleException(string message)
        : base(ApiResultStatusCode.NotExtended, message, HttpStatusCode.NotExtended)
    {
    }

    public NullSampleException(object additionalData)
        : base(ApiResultStatusCode.NotExtended, null, HttpStatusCode.NotExtended, additionalData)
    {
    }

    public NullSampleException(string message, object additionalData)
        : base(ApiResultStatusCode.NotExtended, message, HttpStatusCode.NotExtended, additionalData)
    {
    }

    public NullSampleException(string message, Exception exception)
        : base(ApiResultStatusCode.NotExtended, message, exception, HttpStatusCode.NotExtended)
    {
    }

    public NullSampleException(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.NotExtended, message, HttpStatusCode.NotExtended, exception, additionalData)
    {
    }
}
public class AppArgumentException : AppException
{

    public AppArgumentException()
        : base(ApiResultStatusCode.NotExtended, HttpStatusCode.NotExtended)
    {
    }

    public AppArgumentException(string message)
        : base(ApiResultStatusCode.NotExtended, message, HttpStatusCode.NotExtended)
    {
    }

    public AppArgumentException(object additionalData)
        : base(ApiResultStatusCode.NotExtended, null, HttpStatusCode.NotExtended, additionalData)
    {
    }

    public AppArgumentException(string message, object additionalData)
        : base(ApiResultStatusCode.NotExtended, message, HttpStatusCode.NotExtended, additionalData)
    {
    }

    public AppArgumentException(string message, Exception exception)
        : base(ApiResultStatusCode.NotExtended, message, exception, HttpStatusCode.NotExtended)
    {
    }

    public AppArgumentException(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.NotExtended, message, HttpStatusCode.NotExtended, exception, additionalData)
    {
    }
}
