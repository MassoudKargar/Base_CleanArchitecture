﻿namespace Base.Samples.EndPoints.WebApi.People;

public class PeopleController(IGenericService<Person, long> personRepository, ILogger<PeopleController> logger)
    : GenericController<Person, long, PersonListViewModel, PersonUpdateViewModel, PersonUpdateValidator, PersonInsertViewModel, PersonInsertValidator, PersonSelectViewModel>(personRepository, logger){}