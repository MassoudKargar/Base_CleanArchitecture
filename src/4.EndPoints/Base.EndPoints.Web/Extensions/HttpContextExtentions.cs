namespace Base.EndPoints.Web.Extensions;

public static class HttpContextExtensions
{
    public static BaseServices BaseApplicationContext(this HttpContext httpContext) =>
        (BaseServices)httpContext.RequestServices.GetService(typeof(BaseServices));
}