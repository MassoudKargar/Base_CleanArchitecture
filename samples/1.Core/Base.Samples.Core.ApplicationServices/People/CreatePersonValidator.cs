using Base.Samples.Core.Contracts.People;

namespace Base.Samples.Core.ApplicationServices.People;
public class PersonValidator : AbstractValidator<PersonDto>
{
    public PersonValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}