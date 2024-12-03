using Base.Extensions.ObjectMappers.AutoMapper.Services;

namespace Base.Application.Common;


/// <summary>
/// کلاس پایه‌ای برای DTO که فرآیند تبدیل داده‌ها بین موجودیت‌ها (Entities) و DTO ها را مدیریت می‌کند.
/// این کلاس از AutoMapper برای انجام نگاشت‌ها استفاده می‌کند و امکان نگاشت‌های سفارشی را فراهم می‌کند.
/// </summary>
/// <typeparam name="TDto">نوع DTO که برای انتقال داده‌ها استفاده می‌شود.</typeparam>
/// <typeparam name="TEntity">نوع موجودیت که داده‌ها از آن استخراج و به آن تبدیل می‌شود.</typeparam>
/// <typeparam name="TKey">نوع شناسه موجودیت که معمولاً از آن برای شناسایی موجودیت‌ها استفاده می‌شود.</typeparam>
public abstract class BaseDto<TDto, TEntity, TKey> : IHaveCustomMapping
    where TDto : class, new()
    where TEntity : BaseEntity<TKey>, new()
    where TKey : struct
{
    /// <summary>
    /// تبدیل DTO به موجودیت.
    /// </summary>
    /// <param name="mapper">ابزار نگاشت داده‌ها</param>
    /// <returns>موجودیتی که از DTO ساخته شده است.</returns>
    public TEntity ToEntity(IMapper mapper)
    {
        return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
    }

    /// <summary>
    /// تبدیل DTO به موجودیت و بروزرسانی موجودیت موجود.
    /// </summary>
    /// <param name="mapper">ابزار نگاشت داده‌ها</param>
    /// <param name="entity">موجودیت موجود برای بروزرسانی</param>
    /// <returns>موجودیتی که بروزرسانی شده است.</returns>
    public TEntity ToEntity(IMapper mapper, TEntity entity)
    {
        return mapper.Map(CastToDerivedClass(mapper, this), entity);
    }

    /// <summary>
    /// تبدیل موجودیت به DTO.
    /// </summary>
    /// <param name="mapper">ابزار نگاشت داده‌ها</param>
    /// <param name="model">موجودیتی که باید به DTO تبدیل شود</param>
    /// <returns>DTO که از موجودیت ایجاد شده است.</returns>
    public static TDto FromEntity(IMapper mapper, TEntity model)
    {
        return mapper.Map<TDto>(model);
    }

    /// <summary>
    /// تبدیل کلاس پایه به کلاس مشتق‌شده.
    /// </summary>
    /// <param name="mapper">ابزار نگاشت داده‌ها</param>
    /// <param name="baseInstance">کلاس پایه‌ای که باید به کلاس مشتق‌شده تبدیل شود.</param>
    /// <returns>کلاس مشتق‌شده</returns>
    protected TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity, TKey> baseInstance)
    {
        return mapper.Map<TDto>(baseInstance);
    }

    /// <summary>
    /// ایجاد نگاشت‌های بین DTO و موجودیت از طریق AutoMapper.
    /// </summary>
    /// <param name="profile">پروفایل نگاشت در AutoMapper</param>
    public void CreateMappings(Profile profile)
    {
        profile.AllowNullCollections = true;
        var mappingExpression = profile.CreateMap<TDto, TEntity>();

        var dtoType = typeof(TDto);
        var entityType = typeof(TEntity);
        foreach (var property in entityType.GetProperties(System.Reflection.BindingFlags.Static))
        {
            var p = dtoType.GetProperty(property.Name);
            if (p == null)
                mappingExpression.ForMember(property.Name, opt => opt.Ignore());
        }

        CustomMappings(mappingExpression.ReverseMap());
    }

    /// <summary>
    /// نگاشت‌های سفارشی را انجام می‌دهد که در کلاس‌های مشتق‌شده از BaseDto قابل پیاده‌سازی است.
    /// </summary>
    /// <param name="mapping">نگاشت بین موجودیت و DTO</param>
    public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
    {
    }
}
