using Base.Infra.Validators.Abstractions.Abstractions;

namespace Base.Sample.Application.People.Validators;

public class PersonInsertViewModelValidator : BaseValidator<PersonInsertViewModel>
{
    public PersonInsertViewModelValidator()
    {
        RuleForPropHavingValue(c => c.FirstName);
        RuleForPropHavingValue(c => c.LastName);
    }
}