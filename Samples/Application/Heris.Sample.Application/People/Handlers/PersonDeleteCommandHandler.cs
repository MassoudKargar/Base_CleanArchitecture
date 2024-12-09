namespace Heris.Sample.Application.People.Handlers;

public class PersonDeleteCommandHandler(IGenericRepository<Person, long> service) :
    AbstractDeleteCommandHandler<long, PersonDeleteViewModel, GenericCommand<long, PersonDeleteViewModel>,
        Person>(service);