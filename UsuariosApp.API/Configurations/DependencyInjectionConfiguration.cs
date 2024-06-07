using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Repositories;
using UsuariosApp.Infra.Messages.Producers;

namespace UsuariosApp.API.Configurations
{
    public class DependencyInjectionConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            #region Injeções de dependências

            services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IMessageProducer, MessageProducer>();

            #endregion

        }
    }
}
