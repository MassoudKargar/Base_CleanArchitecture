namespace Heris.EndPoints.Web.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    /// دریافت شیء `BaseServices` از سرویس‌های درخواست HTTP.
    /// این متد به‌صورت اتوماتیک سرویس `BaseServices` را از کانتکست درخواست دریافت می‌کند.
    /// </summary>
    /// <param name="httpContext">شیء `HttpContext` که حاوی اطلاعات درخواست است.</param>
    /// <returns>شیء `BaseServices` که از `RequestServices` دریافت شده است.</returns>
    public static BaseServices BaseApplicationContext(this HttpContext httpContext) => 
        httpContext.RequestServices.GetService(typeof(BaseServices)) as BaseServices ?? throw new InvalidOperationException();

    /// <summary>
    /// دریافت شیء `IMediator` از سرویس‌های درخواست HTTP.
    /// این متد برای ارسال درخواست‌های Mediator از درخواست HTTP استفاده می‌کند.
    /// </summary>
    /// <param name="httpContext">شیء `HttpContext` که حاوی اطلاعات درخواست است.</param>
    /// <returns>شیء `IMediator` که از `RequestServices` دریافت شده است.</returns>
    public static IMediator MediatRDispatcher(this HttpContext httpContext) => 
        httpContext.RequestServices.GetService(typeof(IMediator)) as IMediator ?? throw new InvalidOperationException();

    /// <summary>
    /// دریافت شیء `IMapper` از سرویس‌های درخواست HTTP.
    /// این متد برای دریافت `IMapper` از کانتکست درخواست HTTP استفاده می‌شود.
    /// </summary>
    /// <param name="httpContext">شیء `HttpContext` که حاوی اطلاعات درخواست است.</param>
    /// <returns>شیء `IMapper` که از `RequestServices` دریافت شده است.</returns>
    public static IMapper MapperDispatcher(this HttpContext httpContext) => 
        httpContext.RequestServices.GetService(typeof(IMapper)) as IMapper ?? throw new InvalidOperationException();
}
