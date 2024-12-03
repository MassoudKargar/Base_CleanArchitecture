﻿namespace Base.Core.Domains.AppEvents;
/// <summary>
/// رویدادی که اعلام می‌کند یک موجود جدید ‌ایجاد شده است.
/// </summary>
/// <typeparam name="TEntity">نوع موجود</typeparam>
/// <typeparam name="TId">نوع شناسه</typeparam>
public class EntityInsertedEvent<TEntity, TId>(TEntity entity) : BaseEvent<TEntity, TId>(entity)
    where TEntity : BaseEntity<TId>
    where TId : struct;