namespace Base.Sample.Application.People.ViewModels;

public class PersonSelectViewModel : BaseDto<PersonSelectViewModel, Person, long>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public long StoreId { get; set; }
}