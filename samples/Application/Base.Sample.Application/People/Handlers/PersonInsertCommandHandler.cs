using Base.Sample.Application.Commands.Generics;
using Base.Sample.Application.RequestResponse.Responses;

namespace Base.Sample.Application.People.Handlers;

public class PersonInsertCommandHandler(IGenericRepository<Person?, long> service, IMapper mapper) :
    Base.Sample.Application.Commands.Handlers.Generics.GenericCreateCommandHandler<long, PersonInsertViewModel, GenericCreateCommand<long, PersonInsertViewModel, BaseApiResponse>, BaseApiResponse, Person>(service, mapper);