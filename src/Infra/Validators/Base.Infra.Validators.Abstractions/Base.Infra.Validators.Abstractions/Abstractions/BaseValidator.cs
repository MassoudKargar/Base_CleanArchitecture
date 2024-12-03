using Base.Infra.Validators.Templates;
using FluentValidation;
using System.Linq.Expressions;

namespace Base.Infra.Validators.Abstractions.Abstractions
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public IRuleBuilderOptions<T, TProperty> RuleForPropHavingValue<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var propName = expression.Body.ToString().Split(".")[1];
            return RuleFor(expression).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredProperty(propName));

        }
    }


}
