using Base.Application.BaseMediatR;

namespace Base.Sample.Application.People.Handlers;

public class PersonSelectHandler(IGenericRepository<Person, long> service, IMapper mapper) :
    AbstractQueryHandler<long, PersonSelectViewModel, GenericQuery<long, PersonSelectViewModel, PersonSelectViewModel>, PersonSelectViewModel, Person>(service, mapper);