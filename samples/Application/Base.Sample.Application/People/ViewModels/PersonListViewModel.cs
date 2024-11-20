namespace Base.Sample.Application.People.ViewModels;

public class PersonListViewModel : BaseDto<PersonListViewModel, Person, long>
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
