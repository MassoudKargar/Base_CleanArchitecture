namespace Heris.Sample.Application.People.Validators;

public class PersonInsertViewModelValidator : AbstractValidator<PersonInsertViewModel>
{
    public PersonInsertViewModelValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}