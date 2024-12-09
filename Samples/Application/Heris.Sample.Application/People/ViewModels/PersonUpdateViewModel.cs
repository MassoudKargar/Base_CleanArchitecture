namespace Heris.Sample.Application.People.ViewModels;

public class PersonUpdateViewModel : BaseDto<PersonUpdateViewModel, Person, long>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}