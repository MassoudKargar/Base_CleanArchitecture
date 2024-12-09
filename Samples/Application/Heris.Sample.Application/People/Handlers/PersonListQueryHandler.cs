namespace Heris.Sample.Application.People.Handlers;

public class PersonListQueryHandler(IGenericRepository<Person, long> service, IMapper mapper) :
    AbstractGetAllQueryHandler<long, Person, GenericQuery<long, Person, IEnumerable<PersonListViewModel>>, IEnumerable<PersonListViewModel>, Person>
    (service, mapper);