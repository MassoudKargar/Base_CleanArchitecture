namespace Base.Sample.Application.People.Handlers;

public class PersonListQueryHandler(IGenericRepository<Person, long> service, IMapper mapper) :
    AbstractQueryHandler<long, GenericQuery<long, IEnumerable<PersonListViewModel>>, IEnumerable<PersonListViewModel>, Person>(service, mapper);