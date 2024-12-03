using Base.Infra.Validators.Abstractions.Abstractions;
using FluentValidation.Validators;

namespace Base.Sample.Application.People.Validators;

public class PersonInsertViewModelValidator : BaseValidator<PersonInsertViewModel>
{
    public PersonInsertViewModelValidator()
    {
        RuleForPropHavingMaxLength(x => x.FirstName, 10);
        RuleForPropHavingValue(c => c.FirstName);
        RuleForPropHavingValue(c => c.LastName);
    }
}