using Ardalis.Result.AspNetCore;
using Base.EndPoints.Web.Extensions;
using Base.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Base.EndPoints.Web.Controllers;

/// <summary>
/// کنترلر پایه برای مدیریت درخواست‌های API.
/// این کلاس تمام ویژگی‌ها و متدهای عمومی برای سایر کنترلرهای API را فراهم می‌آورد.
/// </summary>
[ApiController]
[Route("/api/[controller]/[action]")]
[TranslateResultToActionResult]
public class BaseController : Controller
{
    /// <summary>
    /// سرویس‌های پایه برای دسترسی به منطق تجاری و عملیاتی در کنترلرها.
    /// </summary>
    protected BaseServices BaseServices => HttpContext.BaseApplicationContext();
}
