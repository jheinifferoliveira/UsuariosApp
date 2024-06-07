using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        void CriarConta(Usuario usuario);

        Usuario? Autenticar(string emai, string senha);

        Usuario? ObterDados(Guid id);
    }
}
