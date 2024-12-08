using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Commands.Generics
{
    public class GenericUpdateCommand<TId, TViewModel, TResponse> : IRequest<TResponse>
        where TId : struct
    {
        public TId Id { get; }

        public TViewModel Model { get; }

        public GenericUpdateCommand(TId id, TViewModel model)
        {
            Id = id;
            Model = model;
        }
    }
}
