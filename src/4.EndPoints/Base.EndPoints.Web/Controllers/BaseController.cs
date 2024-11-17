namespace Base.EndPoints.Web.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
[ApiResultFilter]

public class BaseController : Controller
{
    protected BaseServices BaseServices => HttpContext.BaseApplicationContext();
}