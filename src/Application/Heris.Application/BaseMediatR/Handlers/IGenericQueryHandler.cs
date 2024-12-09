using Heris.Application.BaseMediatR.Commands;

namespace Heris.Application.BaseMediatR.Handlers;

/// <summary>
/// این اینترفیس برای پردازش درخواست‌های جستجو و بازیابی داده‌ها طراحی شده است.
/// </summary>
/// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
/// <typeparam name="TQuery">نوع کوئری که جزئیات جستجو را مشخص می‌کند</typeparam>
/// <typeparam name="TRequest">نوع درخواست که حاوی اطلاعات جستجو است</typeparam>
/// <typeparam name="TResponse">نوع پاسخ که نتایج جستجو را در بر می‌گیرد</typeparam>
public interface IGenericQueryHandler<TId, TQuery, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : GenericQuery<TId, TQuery, TResponse>
    where TId : struct
    where TResponse : class
{
    // این اینترفیس مانند IRequestHandler متدی ندارد. پیاده‌سازی‌های آن باید متد Handle را برای پردازش درخواست پیاده‌سازی کنند.
}
