using Base.Application.BaseMediatR;
namespace Base.Sample.Application.People.Handlers;

public class PersonDeleteCommandHandler(IGenericRepository<Person?, long> service, IMapper mapper) :
    AbstractDeleteCommandHandler<long, PersonDeleteViewModel, GenericDeleteCommand<long, PersonDeleteViewModel, Result>, Result,
        Person>(service, mapper);