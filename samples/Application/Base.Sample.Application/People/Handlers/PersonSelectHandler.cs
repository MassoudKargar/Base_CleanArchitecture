namespace Base.Sample.Application.People.Handlers;

public class PersonSelectHandler(IGenericRepository<Person, long> service, IMapper mapper) :
    AbstractQueryHandler<long, GenericQuery<long, PersonSelectViewModel>, PersonSelectViewModel, Person>(service, mapper);