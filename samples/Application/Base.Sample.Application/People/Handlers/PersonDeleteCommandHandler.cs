using Base.Application.BaseMediatR;
using Base.Sample.Application.Commands.Generics;
namespace Base.Sample.Application.People.Handlers;

public class PersonDeleteCommandHandler(IGenericRepository<Person?, long> service, IMapper mapper) :
    Base.Sample.Application.Commands.Handlers.Generics.GenericDeleteCommandHandler<long, PersonDeleteViewModel, GenericDeleteCommand<long, PersonDeleteViewModel, Result>, Result,
        Person>(service, mapper);