using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Entities
{
    public class Perfil
    {
        /// <summary>
        /// Modelo de entidade de domínio.
        /// </summary>
        #region Propriedades

        public Guid? Id { get; set; }
        public string? Nome { get; set; }

        #endregion

        #region Relacionamentos

        public List<Usuario>? Usuarios { get; set; }

        #endregion
    }
}
