using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Commands.Handlers.Contracts
{
    public interface IGenericCreateCommandHandler<TId, TViewModel, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : Base.Application.Commands.Generics.GenericCreateCommand<TId, TViewModel, TResponse>
        where TId : struct
    {
        // این اینترفیس هیچ متدی تعریف نمی‌کند و فقط از IRequestHandler استفاده می‌کند
        // به این معنا که هر پیاده‌سازی باید متد Handle را برای پردازش درخواست پیاده‌سازی کند.
    }
}
