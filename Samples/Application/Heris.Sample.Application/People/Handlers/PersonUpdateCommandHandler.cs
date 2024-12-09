namespace Heris.Sample.Application.People.Handlers;

public class PersonUpdateCommandHandler(IGenericRepository<Person, long> service, IMapper mapper) :
    AbstractUpdateCommandHandler<long,PersonUpdateViewModel,GenericCommand<long, PersonUpdateViewModel>,Person>(service, mapper);