using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using mzm_safelink.application.dto.url.actions;

namespace mzm_safelink.ioc
{
    public static class ValidatorsConfigIoC
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UrlCreateDTO>, UrlCreateDTOValidator>();

            return services;
        }
    }
}