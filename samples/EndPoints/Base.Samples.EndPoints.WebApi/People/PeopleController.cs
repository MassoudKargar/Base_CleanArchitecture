namespace Base.Samples.EndPoints.WebApi.People;

public class PeopleController(ILogger<PeopleController> logger)
    : GenericController<Person, long, PersonListViewModel, PersonUpdateViewModel, PersonInsertViewModel, PersonDeleteViewModel, PersonSelectViewModel>(logger);
