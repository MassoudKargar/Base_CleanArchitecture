using Base.Application.BaseMediatR;

namespace Base.Sample.Application.People.Handlers;

public class PersonListQueryHandler(IGenericRepository<Person, long> service, IMapper mapper) :
    AbstractQueryHandler<long, Person, GenericQuery<long, Person, IEnumerable<PersonListViewModel>>, IEnumerable<PersonListViewModel>, Person>(service, mapper);