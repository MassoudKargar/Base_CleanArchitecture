using Ardalis.Result;
using Base.Application.BaseMediatR;
using Base.Application.Common;
using Base.Core.Domains.Entities;
using Base.EndPoints.Web.Extensions;
using Base.Sample.Application.Commands.Generics;
using Base.Sample.Application.RequestResponse.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Base.Samples.EndPoints.WebApi.Controllers;


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
    public async Task<dynamic> GetAllAsync(ODataQueryOptions<TEntity> queryOptions, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetAll {typeof(TEntity).FullName}");
        var result = await Mediator.Send(new GenericQuery<TId, TEntity, IEnumerable<TListViewModel>>
            (default, queryOptions, GenericAction.GetAll), cancellationToken);
        return Result.Success(result);
    }

    /// <summary>
    /// دریافت موجودیت با شناسه مشخص.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>مدل نمایشی موجودیت</returns>
    [HttpGet("{id}")]
    public async Task<dynamic> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetById {typeof(TEntity).FullName}");
        var result = await Mediator.Send(new GenericQuery<TId, TSelectViewModel, TSelectViewModel>(id, null, GenericAction.GetById), cancellationToken);
        return Result.Success(result);
    }

    /// <summary>
    /// افزودن موجودیت جدید.
    /// </summary>
    /// <param name="dto">مدل نمایشی برای درج</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>نتیجه عملیات</returns>
    [HttpPost]
    public async Task<dynamic> AddAsync([FromBody] TInsertViewModel dto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Insert {typeof(TEntity).FullName}");
        var result = await Mediator.Send(new GenericCreateCommand<TId, TInsertViewModel, BaseCommandResult>(default, dto), cancellationToken);
        return Result.Created(result);
    }

    /// <summary>
    /// بروزرسانی موجودیت با شناسه مشخص.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="dto">مدل نمایشی برای بروزرسانی</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>نتیجه عملیات</returns>
    [HttpPost("{id}")]
    public async Task<dynamic> UpdateAsync(TId id, [FromBody] TUpdateViewModel dto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Update {typeof(TEntity).FullName}");
        var result = await Mediator.Send(new GenericUpdateCommand<TId, TUpdateViewModel, Result>(id, dto), cancellationToken);
        return Result.Success(result);
    }

    /// <summary>
    /// حذف موجودیت با شناسه مشخص.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>نتیجه عملیات</returns>
    [HttpPost("{id}")]
    public async Task<dynamic> DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Delete {typeof(TEntity).FullName} => Id :{id}");
        var result = await Mediator.Send(new GenericDeleteCommand<TId, TDeleteViewModel, Result>(id, default), cancellationToken);
        return Result.Success(result);
    }
}
