using Base.Application.BaseMediatR;
using Base.Sample.Application.Commands.Generics;
using Base.Sample.Application.RequestResponse.Responses;
namespace Base.Sample.Application.People.Handlers;

public class PersonDeleteCommandHandler(IGenericRepository<Person?, long> service, IMapper mapper) :
    Base.Sample.Application.Commands.Handlers.Generics.GenericDeleteCommandHandler<long, PersonDeleteViewModel, GenericDeleteCommand<long, PersonDeleteViewModel, BaseCommandResult>, BaseCommandResult,
        Person>(service, mapper);