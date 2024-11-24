namespace Base.Utility.Exceptions;

public class SampleValidationException : AppException
{
    public List<string> Errors { get; set; } = new();

    public SampleValidationException(FluentValidation.Results.ValidationResult validationResult)
        : base(ApiResultStatusCode.BadRequest, HttpStatusCode.BadRequest)
    {
        Errors.Capacity = validationResult.Errors.Select(s => s.ErrorMessage).Count();
        Errors.AddRange(validationResult.Errors.Select(s => s.ErrorMessage));
    }
    public SampleValidationException()
        : base(ApiResultStatusCode.BadRequest, HttpStatusCode.BadRequest)
    {
    }

    public SampleValidationException(string message)
        : base(ApiResultStatusCode.BadRequest, message, HttpStatusCode.BadRequest)
    {
    }

    public SampleValidationException(object additionalData)
        : base(ApiResultStatusCode.BadRequest, null, HttpStatusCode.BadRequest, additionalData)
    {
    }

    public SampleValidationException(string message, object additionalData)
        : base(ApiResultStatusCode.BadRequest, message, HttpStatusCode.BadRequest, additionalData)
    {
    }

    public SampleValidationException(string message, Exception exception)
        : base(ApiResultStatusCode.BadRequest, message, exception, HttpStatusCode.BadRequest)
    {
    }

    public SampleValidationException(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.BadRequest, message, HttpStatusCode.BadRequest, exception, additionalData)
    {
    }
}