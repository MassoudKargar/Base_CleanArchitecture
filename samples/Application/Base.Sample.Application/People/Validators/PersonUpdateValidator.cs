using Base.Infra.Validators.Abstractions.Abstractions;

namespace Base.Sample.Application.People.Validators;

public class PersonUpdateValidator : BaseValidator<PersonUpdateViewModel>
{
    public PersonUpdateValidator()
    {
        RuleForPropHavingValue(c => c.FirstName);
        RuleForPropHavingValue(c => c.LastName);
    }
}