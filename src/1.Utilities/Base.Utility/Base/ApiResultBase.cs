namespace Base.Utility.Base;

public class ApiResultBase(bool isSuccess, ApiResultStatusCode statusCode, string? message = null)
{
    public bool IsSuccess { get; set; } = isSuccess;
    public ApiResultStatusCode StatusCode { get; set; } = statusCode;
    public string? Message { get; set; } = message;
}
public class ApiResultBase<TData>(bool isSuccess, ApiResultStatusCode statusCode, TData result, string? message = null)
    : ApiResultBase(isSuccess, statusCode, message)
{
    public TData Result { get; set; } = result;
}