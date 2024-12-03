using Base.Application.BaseMediatR;

namespace Base.Sample.Application.People.Handlers;

public class PersonUpdateCommandHandler(IGenericRepository<Person?, long> service, IMapper mapper) :
    AbstractCommandHandler<long,PersonUpdateViewModel,GenericCommand<long, PersonUpdateViewModel, Result>,Result,Person>(service, mapper);