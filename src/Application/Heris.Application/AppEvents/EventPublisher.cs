using Microsoft.Extensions.Logging;

namespace Heris.Application.AppEvents;
/// <summary>
/// نماینده پیاده‌سازی ناشر رویداد
/// </summary>
public partial class EventPublisher(IServiceProvider serviceProvider) : IEventPublisher, ITransientLifetime
{
    private IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// رویداد را به مصرف‌کنندگان ارسال می‌کند
    /// </summary>
    /// <typeparam name="TEvent">نوع رویداد</typeparam>
    /// <typeparam name="TEntity">نوع موجودیت</typeparam>
    /// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
    /// <param name="event">شیء رویداد</param>
    public virtual async Task PublishAsync<TEvent, TEntity, TId>(TEvent @event)
        where TEvent : BaseEvent<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        // دریافت تمام مصرف‌کنندگان رویداد
        var consumers = ResolveAll<IConsumer<TEvent, TEntity, TId>>().ToList();
        foreach (var consumer in consumers)
        {
            // تلاش برای پردازش رویداد منتشرشده
            await consumer.HandleEventAsync(@event);
        }
    }

    /// <summary>
    /// رویداد را به مصرف‌کنندگان ارسال می‌کند (نسخه همزمان)
    /// </summary>
    /// <typeparam name="TEvent">نوع رویداد</typeparam>
    /// <typeparam name="TEntity">نوع موجودیت</typeparam>
    /// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
    /// <param name="event">شیء رویداد</param>
    public virtual void Publish<TEvent, TEntity, TId>(TEvent @event)
        where TEvent : BaseEvent<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        // دریافت تمام مصرف‌کنندگان رویداد
        var consumers = ResolveAll<IConsumer<TEvent, TEntity, TId>>().ToList();
        foreach (var consumer in consumers)
        {
            consumer.HandleEventAsync(@event);
        }
    }

    /// <summary>
    /// سرویس‌دهی به مصرف‌کنندگان رویداد از طریق دسترسی به خدمات
    /// </summary>
    /// <param name="scope">دامنه سرویس</param>
    /// <returns>سرویس‌دهی به مصرف‌کنندگان</returns>
    protected IServiceProvider GetServiceProvider()
    {
        var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
        var context = accessor?.HttpContext;
        return context?.RequestServices ?? ServiceProvider;
    }

    /// <summary>
    /// تمام نمونه‌های یک نوع را از سرویس‌ها دریافت می‌کند
    /// </summary>
    /// <typeparam name="T">نوع سرویس</typeparam>
    /// <returns>تمام نمونه‌ها از نوع مشخص‌شده</returns>
    public virtual IEnumerable<T> ResolveAll<T>()
    {
        return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
    }
}
