namespace Base.EndPoints.Web.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
[TranslateResultToActionResult]
public class BaseController : Controller
{
    protected BaseServices BaseServices => HttpContext.BaseApplicationContext();
}