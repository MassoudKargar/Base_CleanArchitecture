using FluentValidation;

namespace Base.Samples.EndPoints.WebApi.Validator
{
    public static class FluentValidationExtenstion
    {
        public static void InitializeValidators(this IServiceCollection services, Type InitType)
        {
            services.AddValidatorsFromAssemblyContaining(InitType);
        }

        public static void RegisterValidator<TModel, TValidator>(this IServiceCollection services) where TValidator : AbstractValidator<TModel>
        {
            services.AddScoped<IValidator<TModel>, TValidator>();

        }
    }
}
