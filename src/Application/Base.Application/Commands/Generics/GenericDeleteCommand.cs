﻿namespace Base.Application.Commands.Generics
{
    public class GenericDeleteCommand<TId, TViewModel, TResponse> : IRequest<TResponse>
        where TId : struct
    {
        /// <summary>
        /// شناسه موجودیت
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// مدل داده‌ای برای موجودیت که در عملیات استفاده می‌شود
        /// </summary>
        public TViewModel Model { get; }

        /// <summary>
        /// نوع عملیاتی که باید انجام شود (درج، به‌روزرسانی، حذف و...)
        /// </summary>

        /// <summary>
        /// سازنده کلاس GenericCommand
        /// </summary>
        /// <param name="id">شناسه موجودیت</param>
        /// <param name="model">مدل داده‌ای موجودیت</param>
        public GenericDeleteCommand(TId id, TViewModel model)
        {
            Id = id;
            Model = model;
        }
    }
}
