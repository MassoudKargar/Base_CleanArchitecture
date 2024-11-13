﻿namespace Base.EndPoints.Web.Attributes;

public class ApiResult : ApiResultBase
{
    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string? message = null) : base(isSuccess, statusCode, message)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }
    #region Implicit Operators
    public static implicit operator ApiResult(OkResult result) => new(true, ApiResultStatusCode.OK);

    public static implicit operator ApiResult(BadRequestResult result) => new(false, ApiResultStatusCode.BadRequest);

    public static implicit operator ApiResult(BadRequestObjectResult result)
    {
        var message = result.Value?.ToString();
        if (result.Value is SerializableError errors)
        {
            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
            message = string.Join(" | ", errorMessages);
        }
        return new ApiResult(false, ApiResultStatusCode.BadRequest, message);
    }

    public static implicit operator ApiResult(ContentResult result)
    => new(true, ApiResultStatusCode.OK, result.Content);

    public static implicit operator ApiResult(NotFoundResult result)
    => new(false, ApiResultStatusCode.NotFound);


    #endregion
}

public class ApiResult<TData> : ApiResultBase<TData> where TData : class
{
    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData? data, string? message = null)
        : base(isSuccess, statusCode, data, message)
    {
        Result = data;
        //Schema = JsonSchema.FromType(data?.GetType());
    }

    #region Implicit Operators
    public static implicit operator ApiResult<TData>(TData data)
        => new(true, ApiResultStatusCode.OK, data);

    public static implicit operator ApiResult<TData>(OkResult result)
    => new(true, ApiResultStatusCode.OK, null);

    public static implicit operator ApiResult<TData>(OkObjectResult result)
    => new(true, ApiResultStatusCode.OK, result.Value as TData);

    public static implicit operator ApiResult<TData>(BadRequestResult result)
    => new(false, ApiResultStatusCode.BadRequest, null);

    public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
    {
        var message = result.Value?.ToString();
        if (result.Value is not SerializableError errors)
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, message);
        var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
        message = string.Join(" | ", errorMessages);
        return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, message);
    }

    public static implicit operator ApiResult<TData>(ContentResult result)
    => new(true, ApiResultStatusCode.OK, null, result.Content);

    public static implicit operator ApiResult<TData>(NotFoundResult result)
    => new(false, ApiResultStatusCode.NotFound, null);

    public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
    => new(false, ApiResultStatusCode.NotFound, result.Value as TData);
    #endregion
}