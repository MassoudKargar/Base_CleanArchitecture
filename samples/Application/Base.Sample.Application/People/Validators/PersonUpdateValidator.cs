namespace Base.Sample.Application.People.Validators;

public class PersonUpdateValidator : AbstractValidator<PersonUpdateViewModel>
{
    public PersonUpdateValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().NotNull();
        RuleFor(c => c.LastName).NotEmpty().NotNull();
    }
}