using FluentValidation;

namespace Base.Samples.EndPoints.WebApi.Validator
{
    public class FluentValidationExtenstion
    {
        public static void Init(IServiceCollection services, Type InitType)
        {
            services.AddValidatorsFromAssemblyContaining(InitType);
        }

        public static void RegisterValidator<TModel, TValidator>(IServiceCollection services) where TValidator : AbstractValidator<TModel>
        {
            services.AddScoped<IValidator<TModel>, TValidator>();

        }
    }
}
