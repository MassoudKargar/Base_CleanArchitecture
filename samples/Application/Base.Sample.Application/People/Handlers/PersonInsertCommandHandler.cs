using Base.Application.BaseMediatR;

namespace Base.Sample.Application.People.Handlers;

public class PersonInsertCommandHandler(IGenericRepository<Person?, long> service, IMapper mapper) :
    AbstractCommandHandler<long, PersonInsertViewModel, GenericCommand<long, PersonInsertViewModel, Result>, Result, Person>(service, mapper);