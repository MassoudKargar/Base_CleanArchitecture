namespace Base.Utility.Exceptions;

public class SampleValidationException : AppException
{
    public List<string> Errors { get; set; } = new();

    public SampleValidationException(FluentValidation.Results.ValidationResult validationResult)
        : base(ApiResultStatusCode.NotAcceptable, HttpStatusCode.NotAcceptable)
    {
        Errors.Capacity = validationResult.Errors.Select(s => s.ErrorMessage).Count();
        Errors.AddRange(validationResult.Errors.Select(s => s.ErrorMessage));
    }
    public SampleValidationException()
        : base(ApiResultStatusCode.NotAcceptable, HttpStatusCode.NotAcceptable)
    {
    }

    public SampleValidationException(string message)
        : base(ApiResultStatusCode.NotAcceptable, message, HttpStatusCode.NotAcceptable)
    {
    }

    public SampleValidationException(object additionalData)
        : base(ApiResultStatusCode.NotAcceptable, null, HttpStatusCode.NotAcceptable, additionalData)
    {
    }

    public SampleValidationException(string message, object additionalData)
        : base(ApiResultStatusCode.NotAcceptable, message, HttpStatusCode.NotAcceptable, additionalData)
    {
    }

    public SampleValidationException(string message, Exception exception)
        : base(ApiResultStatusCode.NotAcceptable, message, exception, HttpStatusCode.NotAcceptable)
    {
    }

    public SampleValidationException(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.NotAcceptable, message, HttpStatusCode.NotAcceptable, exception, additionalData)
    {
    }
}