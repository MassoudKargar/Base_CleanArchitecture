using Base.Sample.Application.Commands.Generics;

namespace Base.Sample.Application.People.Handlers;

public class PersonInsertCommandHandler(IGenericRepository<Person?, long> service, IMapper mapper) :
    Base.Sample.Application.Commands.Handlers.Generics.GenericCreateCommandHandler<long, PersonInsertViewModel, GenericCreateCommand<long, PersonInsertViewModel, Result>, Result, Person>(service, mapper);