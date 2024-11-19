﻿namespace Base.Utility.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public ApiResultStatusCode ApiStatusCode { get; set; }
    public object AdditionalData { get; set; }

    public AppException()
        : this(ApiResultStatusCode.NotAcceptable)
    {
    }

    public AppException(ApiResultStatusCode statusCode)
        : this(statusCode, string.Empty)
    {
    }

    public AppException(string message)
        : this(ApiResultStatusCode.NotAcceptable, message)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message)
        : this(statusCode, message, HttpStatusCode.NotAcceptable)
    {
    }

    public AppException(string message, object additionalData)
        : this(ApiResultStatusCode.NotAcceptable, message, additionalData)
    {
    }

    public AppException(ApiResultStatusCode statusCode, object additionalData)
        : this(statusCode, string.Empty, additionalData)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message, object additionalData)
        : this(statusCode, message, HttpStatusCode.NotAcceptable, additionalData)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
        : this(statusCode, message, httpStatusCode, string.Empty)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
        : this(statusCode, message, httpStatusCode, null, additionalData)
    {
    }

    public AppException(string message, Exception exception)
        : this(ApiResultStatusCode.NotAcceptable, message, exception)
    {
    }

    public AppException(string message, Exception exception, object additionalData)
        : this(ApiResultStatusCode.NotAcceptable, message, exception, additionalData)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message, Exception exception)
        : this(statusCode, message, HttpStatusCode.NotAcceptable, exception)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message, Exception exception, object additionalData)
        : this(statusCode, message, HttpStatusCode.NotAcceptable, exception, additionalData)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
        : this(statusCode, message, httpStatusCode, exception, null)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object? additionalData)
        : base(message, exception)
    {
        ApiStatusCode = statusCode;
        HttpStatusCode = httpStatusCode;
        AdditionalData = additionalData;
    }
}
