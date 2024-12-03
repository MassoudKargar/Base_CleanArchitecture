using Base.Application.Common;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using System;
using System.Reflection;

namespace Base.Infra.Validator
{
    public static class FluentValidationExtenstion
    {
        public static void InitializeValidator(this IServiceCollection services)
                                               => services.AddFluentValidationAutoValidation();

        public static void InitializeValidator(this IServiceCollection services, Assembly targetAssembly, Assembly vmAssembly)
        {
            InitializeValidator(services);
            RegisterValidatorsByAssembly(services, targetAssembly, vmAssembly);
        }

        public static void RegisterValidator<TModel, TValidator>(this IServiceCollection services) where TValidator : AbstractValidator<TModel>
        {
            services.AddScoped<IValidator<TModel>, TValidator>();

        }

        public static void RegisterValidatorsByAssembly(this IServiceCollection services, Assembly ValidatorAssembly, Assembly vmAssembly)
        {
            var viewModelList = vmAssembly.GetTypes()
                                     .Where(x => x.IsClass)
                                     .Where(x => x.Assembly == vmAssembly)
                                     .ToList();

            foreach (var viewModel in viewModelList)
            {
                var type = viewModel.GetType();

                var validator = ValidatorAssembly.GetTypes().Where(x => x.IsSubclassOf(typeof(AbstractValidator<>).MakeGenericType(viewModel))).FirstOrDefault();

                if (validator != null)
                    services.AddScoped(typeof(IValidator<>).MakeGenericType(viewModel), validator);

            }


        }
    }
}
