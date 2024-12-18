﻿namespace Heris.EndPoints.Web.Controllers;


/// <summary>
/// کنترلر عمومی برای انجام عملیات مختلف مانند افزودن، بروزرسانی، حذف و گرفتن موجودیت‌ها.
/// این کنترلر از جنریک‌ها برای انواع مختلف داده‌ها استفاده می‌کند.
/// </summary>
/// <typeparam name="TEntity">نوع موجودیت</typeparam>
/// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
/// <typeparam name="TListViewModel">مدل نمایشی برای لیست</typeparam>
/// <typeparam name="TUpdateViewModel">مدل نمایشی برای بروزرسانی</typeparam>
/// <typeparam name="TInsertViewModel">مدل نمایشی برای درج</typeparam>
/// <typeparam name="TDeleteViewModel">مدل نمایشی برای حذف</typeparam>
/// <typeparam name="TSelectViewModel">مدل نمایشی برای انتخاب</typeparam>
public class GenericController<TEntity, TId, TListViewModel, TUpdateViewModel, TInsertViewModel, TDeleteViewModel, TSelectViewModel>(ILogger logger) : BaseController
    where TEntity : BaseEntity<TId>, new()
    where TInsertViewModel : BaseDto<TInsertViewModel, TEntity, TId>, new()
    where TUpdateViewModel : BaseDto<TUpdateViewModel, TEntity, TId>, new()
    where TSelectViewModel : BaseDto<TSelectViewModel, TEntity, TId>, new()
    where TListViewModel : BaseDto<TListViewModel, TEntity, TId>, new()
    where TDeleteViewModel : BaseDto<TDeleteViewModel, TEntity, TId>, new()
    where TId : struct
{
    /// <summary>
    /// دسترسی به Mediator برای ارسال دستورات و درخواست‌ها
    /// </summary>
    protected IMediator Mediator => HttpContext.MediatRDispatcher();

    /// <summary>
    /// دریافت تمامی موجودیت‌ها با استفاده از درخواست‌های OData.
    /// </summary>
    /// <param name="queryOptions">تنظیمات درخواست OData</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>لیستی از مدل‌های نمایشی</returns>
    [HttpGet]
    public async Task<IEnumerable<TListViewModel>> GetAllAsync(ODataQueryOptions<TEntity> queryOptions, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetAll {typeof(TEntity).FullName}");
        var result = await Mediator.Send(new GenericQuery<TId, TEntity, IEnumerable<TListViewModel>>
            (default, queryOptions), cancellationToken);
        return result;
    }

    /// <summary>
    /// دریافت موجودیت با شناسه مشخص.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>مدل نمایشی موجودیت</returns>
    [HttpGet("{id}")]
    public Task<TSelectViewModel> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetById {typeof(TEntity).FullName}");
        var result = Mediator.Send(new GenericQuery<TId, TSelectViewModel, TSelectViewModel>(id, null), cancellationToken);
        return result;
    }

    /// <summary>
    /// افزودن موجودیت جدید.
    /// </summary>
    /// <param name="dto">مدل نمایشی برای درج</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>نتیجه عملیات</returns>
    [HttpPost]
    public Task<TListViewModel> AddAsync([FromBody] TInsertViewModel dto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Insert {typeof(TEntity).FullName}");
        return Mediator.Send(new GenericCommand<TId, TInsertViewModel, TListViewModel>(default, dto), cancellationToken);
    }

    /// <summary>
    /// بروزرسانی موجودیت با شناسه مشخص.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="dto">مدل نمایشی برای بروزرسانی</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>نتیجه عملیات</returns>
    [HttpPost("{id}")]
    public Task UpdateAsync(TId id, [FromBody] TUpdateViewModel dto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Update {typeof(TEntity).FullName}");
        return Mediator.Send(new GenericCommand<TId, TUpdateViewModel>(id, dto), cancellationToken);
    }

    /// <summary>
    /// حذف موجودیت با شناسه مشخص.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>نتیجه عملیات</returns>
    [HttpPost("{id}")]
    public Task DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Delete {typeof(TEntity).FullName} => Id :{id}");
        return Mediator.Send(new GenericCommand<TId, TDeleteViewModel>(id, new TDeleteViewModel()), cancellationToken);
    }
}
