using Ergo.Fit.Service.DepartamentoService;
using Ergo.Fit.Service.TokenService;

namespace Ergo.Fit.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar todos os serviços da aplicação
            services.AddScoped<IDepartamentoInterface, DepartamentoService>();
            services.AddScoped<ITokenService, TokenService>();
            // Adicione outros serviços aqui

            return services;
        }

    }
}
