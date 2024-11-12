namespace Base.Core.RequestResponse.Exceptions;
public class ValidationException : ApplicationException
{
    public List<string> Errors { get; set; } = new();

    public ValidationException(FluentValidation.Results.ValidationResult validationResult)
    {
        Errors.Capacity = validationResult.Errors.Select(s => s.ErrorMessage).Count();
        Errors.AddRange(validationResult.Errors.Select(s => s.ErrorMessage));
    }
}
