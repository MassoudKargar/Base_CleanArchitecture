namespace Heris.Sample.Application.People.Handlers;

public class PersonSelectHandler(IGenericRepository<Person, long> service, IMapper mapper) :
    AbstractGetAllQueryHandler<long, PersonSelectViewModel, GenericQuery<long, PersonSelectViewModel, PersonSelectViewModel>, PersonSelectViewModel, Person>
    (service, mapper);