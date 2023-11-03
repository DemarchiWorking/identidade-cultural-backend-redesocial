using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model.DTOs
{
    public class UsuarioToken
    {
        public bool Autenticado { get; set; }
        public DateTime Expiracao { get; set; }
        public string Token { get; set; }
        public string Mensagem { get; set; }
        public string Usuario { get; set; }
    }
}
