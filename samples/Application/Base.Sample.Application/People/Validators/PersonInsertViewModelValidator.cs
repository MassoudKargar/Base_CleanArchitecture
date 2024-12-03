namespace Base.Sample.Application.People.Validators;

public class PersonInsertViewModelValidator : AbstractValidator<PersonInsertViewModel>
{
    public PersonInsertViewModelValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().NotNull();
        RuleFor(c => c.LastName).NotEmpty().NotNull();
    }
}