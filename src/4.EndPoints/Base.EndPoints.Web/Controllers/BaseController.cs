namespace Base.EndPoints.Web.Controllers;

public class BaseController : Controller
{
    protected BaseServices BaseServices => HttpContext.BaseApplicationContext();

}