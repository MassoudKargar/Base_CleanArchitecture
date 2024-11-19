namespace Base.Sample.Application.People.Validators;

public class PersonInsertValidator : AbstractValidator<PersonInsertViewModel>
{
    public PersonInsertValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}