namespace Base.Sample.Application.People.ViewModels;

public class PersonInsertViewModel : BaseDto<PersonInsertViewModel, Person, long>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
