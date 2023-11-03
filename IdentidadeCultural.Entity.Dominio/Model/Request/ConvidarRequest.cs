using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model.Request
{
    public class ConvidarRequest
    {
        public Guid UsuarioEnviouId { get; set; }
        public Guid UsuarioRecebeuId { get; set; }

    }
}