namespace Base.EndPoints.Web.Extensions;

public static class HttpContextExtensions
{
    public static BaseServices BaseApplicationContext(this HttpContext httpContext) =>
        (BaseServices)httpContext.RequestServices.GetService(typeof(BaseServices));

    public static IMediator MediatRDispatcher(this HttpContext httpContext) =>
        (IMediator)httpContext.RequestServices.GetService(typeof(IMediator));

    public static IMapper MapperDispatcher(this HttpContext httpContext) =>
        (IMapper)httpContext.RequestServices.GetService(typeof(IMapper));
}