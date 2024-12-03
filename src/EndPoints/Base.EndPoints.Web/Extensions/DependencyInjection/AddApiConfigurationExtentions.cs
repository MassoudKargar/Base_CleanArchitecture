using Microsoft.AspNetCore.OData;

namespace Base.EndPoints.Web.Extensions.DependencyInjection;

/// <summary>
/// افزونه‌ای برای پیکربندی خدمات مربوط به API در پروژه.
/// این کلاس شامل متدی است که تنظیمات اولیه برای API را انجام می‌دهد.
/// </summary>
public static class AddApiConfigurationExtensions
{
    /// <summary>
    /// اضافه کردن پیکربندی‌های اصلی برای API به سرویس‌ها.
    /// این متد تنظیمات لازم برای کنترلرها و OData را انجام می‌دهد و همچنین وابستگی‌های پایه‌ای را اضافه می‌کند.
    /// </summary>
    /// <param name="services">مجموعه خدمات که به آن تنظیمات افزوده می‌شود</param>
    /// <param name="assemblyNamesForLoad">آرایه‌ای از نام‌های اسمبلی‌ها که برای بارگذاری وابستگی‌ها استفاده می‌شود</param>
    /// <returns>مجموعه خدمات بعد از انجام تنظیمات</returns>
    public static IServiceCollection AddBaseApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
    {
        // اضافه کردن کنترلرها و فعال کردن ویژگی‌های OData
        services.AddControllers().AddOData(options => options.EnableQueryFeatures());

        // اضافه کردن وابستگی‌های پایه‌ای
        services.AddBaseDependencies(assemblyNamesForLoad);

        return services;
    }
}
