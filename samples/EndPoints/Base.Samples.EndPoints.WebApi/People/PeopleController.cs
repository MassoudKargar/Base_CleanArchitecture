using Base.Sample.Application.People.ViewModels;
using Base.Samples.EndPoints.WebApi.Controllers;

namespace Base.Samples.EndPoints.WebApi.People;

public class PeopleController(ILogger<PeopleController> logger)
    : Base.Samples.EndPoints.WebApi.Controllers.GenericController<Person, long, PersonListViewModel, PersonUpdateViewModel, PersonInsertViewModel, PersonDeleteViewModel, PersonSelectViewModel>(logger);
