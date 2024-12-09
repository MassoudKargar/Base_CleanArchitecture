﻿namespace Heris.Sample.Application.People.Handlers;

public class PersonInsertCommandHandler(IGenericRepository<Person, long> service, IMapper mapper) :
    AbstractInsertCommandHandler<long, PersonInsertViewModel, GenericCommand<long, PersonInsertViewModel>, Person>(service, mapper);