namespace Base.Application.BaseMediatR;
/// <summary>
/// این اینترفیس برای مدیریت درخواست‌های عمومی با استفاده از دستوراتی مانند درج، بروزرسانی، حذف و ... است.
/// </summary>
/// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
/// <typeparam name="TViewModel">مدل نمایش داده برای موجودیت</typeparam>
/// <typeparam name="TRequest">نوع درخواست که حاوی اطلاعات برای پردازش است</typeparam>
/// <typeparam name="TResponse">نوع پاسخ که از پردازش درخواست به دست می‌آید</typeparam>
public interface IGenericCommandHandler<TId, TViewModel, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : GenericCommand<TId, TViewModel, TResponse>
    where TId : struct
{
    // این اینترفیس هیچ متدی تعریف نمی‌کند و فقط از IRequestHandler استفاده می‌کند
    // به این معنا که هر پیاده‌سازی باید متد Handle را برای پردازش درخواست پیاده‌سازی کند.
}
