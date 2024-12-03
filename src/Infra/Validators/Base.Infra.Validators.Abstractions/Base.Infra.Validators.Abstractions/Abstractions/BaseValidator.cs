using Base.Infra.Validators.Templates;
using FluentValidation;
using FluentValidation.Validators;
using System.Collections;
using System.Linq.Expressions;

namespace Base.Infra.Validators.Abstractions.Abstractions
{
    public class BaseValidator<T> : AbstractValidator<T> where T : class
    {
        public IRuleBuilderOptions<T, TProperty> RuleForPropHavingValue<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var propName = expression.Body.ToString().Split(".")[1];
            return RuleFor(expression).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredProperty(propName));


        }

        public IRuleBuilderOptions<T, string> RuleForPropHavingExactLength(Expression<Func<T, string>> expression, int length)
        {
            var propName = expression.Body.ToString().Split(".")[1];
            return RuleFor(expression).SetValidator(new ExactLengthValidator<T>(length)).WithMessage(ErrorMessages.RequiredPropertyLength(propName, length));
        }

        public IRuleBuilderOptions<T, string> RuleForPropHavingMaxLength(Expression<Func<T, string>> expression, int length)
        {
            var propName = expression.Body.ToString().Split(".")[1];
            return RuleFor(expression).SetValidator(new MaximumLengthValidator<T>(length)).WithMessage(ErrorMessages.RequiredMaxPropertyLength(propName, length));
        }


    }


}
