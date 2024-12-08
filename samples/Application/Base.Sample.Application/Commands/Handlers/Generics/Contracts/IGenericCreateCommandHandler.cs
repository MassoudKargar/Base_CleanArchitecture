using Base.Sample.Application.Commands.Generics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Sample.Application.Commands.Handlers.Generics.Contracts
{
    public interface IGenericCreateCommandHandler<TId, TViewModel, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : GenericCreateCommand<TId, TViewModel, TResponse>
        where TId : struct
    {
        // این اینترفیس هیچ متدی تعریف نمی‌کند و فقط از IRequestHandler استفاده می‌کند
        // به این معنا که هر پیاده‌سازی باید متد Handle را برای پردازش درخواست پیاده‌سازی کند.
    }
}
