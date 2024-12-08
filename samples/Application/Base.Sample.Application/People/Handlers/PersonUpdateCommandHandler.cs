using Base.Application.BaseMediatR;
using Base.Sample.Application.Commands.Generics;

namespace Base.Sample.Application.People.Handlers;

public class PersonUpdateCommandHandler(IGenericRepository<Person?, long> service, IMapper mapper) :
    Base.Sample.Application.Commands.Handlers.Generics.GenericUpdateCommandHandler<long,PersonUpdateViewModel, GenericUpdateCommand<long, PersonUpdateViewModel, Result>,Result,Person>(service, mapper);