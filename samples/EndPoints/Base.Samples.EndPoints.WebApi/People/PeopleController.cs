namespace Base.Samples.EndPoints.WebApi.People;

public class PeopleController(IGenericService<Person, long> personRepository, ILogger<PeopleController> logger,IMapper mapper)
    : GenericController<Person, long, PersonListViewModel, PersonUpdateViewModel, PersonUpdateValidator, PersonInsertViewModel, PersonInsertValidator, PersonSelectViewModel>
        (personRepository, logger, mapper);