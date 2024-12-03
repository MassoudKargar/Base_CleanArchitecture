namespace Base.EndPoints.Web.Extensions.DependencyInjection;
/// <summary>
/// این کلاس حاوی مجموعه‌ای از متدهای کمکی برای پیکربندی و اضافه کردن وابستگی‌ها به پروژه است.
/// این متدها به مدیریت تنظیمات مختلف برای وابستگی‌ها، خدمات و پیکربندی‌های پروژه کمک می‌کنند.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// افزودن وابستگی‌های پایه‌ای به سرویس‌ها، شامل دسترسی به داده‌ها، خدمات مفید، تنظیمات MediatR و سایر وابستگی‌ها.
    /// </summary>
    /// <param name="services">مجموعه خدمات که به آن وابستگی‌ها افزوده می‌شود</param>
    /// <param name="assemblyNamesForSearch">آرایه‌ای از نام‌های اسمبلی‌ها برای جستجو و بارگذاری</param>
    /// <returns>مجموعه خدمات بعد از انجام تنظیمات</returns>
    public static IServiceCollection AddBaseDependencies(this IServiceCollection services,
        params string[] assemblyNamesForSearch)
    {
        var assemblies = GetAssemblies(assemblyNamesForSearch);

        services.AddBaseDataAccess(assemblies)
            .AddBaseUtilityServices()
            .AddArdalis()
            .AddCustomDependencies(assemblies)
            .AddMediatR(assemblies);
        services.AddBaseAutoMapperProfiles(option =>
        {
            option.AssemblyNamesForLoadProfiles = "Base";
        });
        return services;

    }
    /// <summary>
    /// افزودن خدمات مفید پایه به سرویس‌ها.
    /// </summary>
    /// <param name="services">مجموعه خدمات که به آن خدمات مفید اضافه می‌شود</param>
    /// <returns>مجموعه خدمات بعد از اضافه کردن خدمات پایه</returns>
    public static IServiceCollection AddBaseUtilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<BaseServices>();
        return services;
    }
    /// <summary>
    /// این متد قواعد پیش‌فرض وضعیت‌های HTTP را بر اساس وضعیت‌های نتیجه‌گیری پیکربندی می‌کند.
    /// این متد نحوه برخورد با کدهای وضعیت HTTP برای انواع مختلف نتیجه‌ها مانند Ok، Created و NoContent را تغییر می‌دهد.
    /// همچنین، نقشه وضعیت‌های Forbidden و Unauthorized از بین می‌رود.
    /// </summary>
    /// <param name="services">شیء `IServiceCollection` که برای ثبت سرویس‌ها در تزریق وابستگی (DI) استفاده می‌شود.</param>
    /// <returns>شیء `IServiceCollection` به‌روزرسانی‌شده برای زنجیره‌سازی متدها.</returns>
    public static IServiceCollection AddArdalis(this IServiceCollection services)
    {
        services.AddControllers(mvcOptions => mvcOptions
            .AddResultConvention(resultStatusMap => resultStatusMap
                .AddDefaultMap()
                .For(ResultStatus.Ok, HttpStatusCode.OK, resultStatusOptions => resultStatusOptions
                    .For("POST", HttpStatusCode.Created)
                    .For("DELETE", HttpStatusCode.NoContent))
                .Remove(ResultStatus.Forbidden)
                .Remove(ResultStatus.Unauthorized)
            ));
        return services;
    }
    /// <summary>
    /// پیکربندی و افزودن تنظیمات مربوط به MediatR به سرویس‌ها.
    /// </summary>
    /// <param name="services">مجموعه خدمات که به آن MediatR اضافه می‌شود</param>
    /// <param name="assemblies">لیست اسمبلی‌هایی که MediatR باید از آن‌ها سرویس‌ها را بارگذاری کند</param>
    /// <returns>مجموعه خدمات بعد از اضافه کردن MediatR</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, List<Assembly> assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
        });

        //services.AddFluentValidationAutoValidation();

        return services;
    }


    /// <summary>
    /// افزودن وابستگی‌های سفارشی به سرویس‌ها.
    /// این متد شامل افزودن وابستگی‌ها با زمان عمر Transient، Scoped و Singleton می‌باشد.
    /// </summary>
    /// <param name="services">مجموعه خدمات که به آن وابستگی‌ها اضافه می‌شود</param>
    /// <param name="assemblies">لیست اسمبلی‌ها برای جستجو و بارگذاری</param>
    /// <returns>مجموعه خدمات بعد از افزودن وابستگی‌های سفارشی</returns>
    public static IServiceCollection AddCustomDependencies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        return services.AddWithTransientLifetime(assemblies, typeof(ITransientLifetime))
            .AddWithScopedLifetime(assemblies, typeof(IScopeLifetime))
            .AddWithSingletonLifetime(assemblies, typeof(ISingletonLifetime));
    }
    /// <summary>
    /// افزودن وابستگی‌هایی با زمان عمر Transient به سرویس‌ها.
    /// </summary>
    /// <param name="services">مجموعه خدمات که به آن وابستگی‌های Transient اضافه می‌شود</param>
    /// <param name="assembliesForSearch">لیست اسمبلی‌ها برای جستجو و بارگذاری</param>
    /// <param name="assignableTo">نوع‌های قابل اختصاص برای این وابستگی‌ها</param>
    /// <returns>مجموعه خدمات بعد از اضافه کردن وابستگی‌های Transient</returns>
    public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        return services;
    }

    /// <summary>
    /// افزودن وابستگی‌هایی با زمان عمر Scoped به سرویس‌ها.
    /// </summary>
    /// <param name="services">مجموعه خدمات که به آن وابستگی‌های Scoped اضافه می‌شود</param>
    /// <param name="assembliesForSearch">لیست اسمبلی‌ها برای جستجو و بارگذاری</param>
    /// <param name="assignableTo">نوع‌های قابل اختصاص برای این وابستگی‌ها</param>
    /// <returns>مجموعه خدمات بعد از اضافه کردن وابستگی‌های Scoped</returns>
    public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
    /// <summary>
    /// افزودن وابستگی‌هایی با زمان عمر Singleton به سرویس‌ها.
    /// </summary>
    /// <param name="services">مجموعه خدمات که به آن وابستگی‌های Singleton اضافه می‌شود</param>
    /// <param name="assembliesForSearch">لیست اسمبلی‌ها برای جستجو و بارگذاری</param>
    /// <param name="assignableTo">نوع‌های قابل اختصاص برای این وابستگی‌ها</param>
    /// <returns>مجموعه خدمات بعد از اضافه کردن وابستگی‌های Singleton</returns>
    public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
        return services;
    }

    /// <summary>
    /// دریافت اسمبلی‌ها بر اساس نام‌های اسمبلی.
    /// </summary>
    /// <param name="assemblyName">آرایه‌ای از نام‌های اسمبلی برای جستجو و بارگذاری</param>
    /// <returns>لیستی از اسمبلی‌های بارگذاری شده</returns>
    public static List<Assembly> GetAssemblies(string[] assemblyName)
    {
        var dependencies = DependencyContext.Default.RuntimeLibraries;
        List<Assembly> list = [];
        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assemblyName))
            {
                list.Add(Assembly.Load(new AssemblyName(library.Name)));
            }
        }

        return list;
    }
    /// <summary>
    /// بررسی اینکه آیا کتابخانه‌ای از اسمبلی‌های انتخابی می‌تواند در جستجو قرار گیرد.
    /// </summary>
    /// <param name="compilationLibrary">کتابخانه‌ کامپایل</param>
    /// <param name="assemblyName">نام‌های اسمبلی برای جستجو</param>
    /// <returns>آیا کتابخانه‌ای مطابق با معیارها پیدا شده است؟</returns>

    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assemblyName)
    {
        return assemblyName.Any(d => compilationLibrary.Name.Contains(d))
            || compilationLibrary.Dependencies.Any(d => assemblyName.Any(c => d.Name.Contains(c)));
    }
}