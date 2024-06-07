using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Models
{
    public class Mensagem
    {
        public string? Destinatario { get; set; }
        public string? Assunto { get; set; }
        public string? Texto { get; set; }
    }
}
