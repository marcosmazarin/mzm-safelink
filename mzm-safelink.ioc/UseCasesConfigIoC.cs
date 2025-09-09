using Microsoft.Extensions.DependencyInjection;
using mzm_safelink.application.interfaces;
using mzm_safelink.application.usecases;

namespace mzm_safelink.ioc
{
    public static class UseCasesConfigIoC
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateUrlShortenUseCase, CreateUrlShortenUseCase>();
            services.AddScoped<IRedirectUrlUseCase, RedirectUrlUseCase>();

            return services;
        }
    }
}