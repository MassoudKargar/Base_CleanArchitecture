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
            return RuleFor(expression).SetValidator(new MaximumLengthValidator<T>(length)).WithMessage(ErrorMessages.RequiredPropertyMaxLength(propName, length));
        }

        public IRuleBuilderOptions<T, string> RuleForPropHavingMinLength(Expression<Func<T, string>> expression, int length)
        {
            var propName = expression.Body.ToString().Split(".")[1];
            return RuleFor(expression).SetValidator(new MinimumLengthValidator<T>(length)).WithMessage(ErrorMessages.RequiredPropertyMinLength(propName, length));
        }

        public IRuleBuilderOptions<T, string> RuleForPropHavingRangeLength(Expression<Func<T, string>> expression, int min,int max)
        {
            var propName = expression.Body.ToString().Split(".")[1];
            return RuleFor(expression).SetValidator(new LengthValidator<T>(min,max)).WithMessage(ErrorMessages.RequiredPropertyInRange(propName, min,max));
        }

    }

    
}
