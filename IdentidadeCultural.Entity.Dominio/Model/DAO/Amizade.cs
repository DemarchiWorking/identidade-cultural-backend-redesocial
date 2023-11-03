using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model
{
    public class Amizade
    {
        //[JsonIgnore]
        public Guid? AmizadeId { get; set; }

        public Guid UsuarioEnviouId { get; set; }
        public Guid UsuarioRecebeuId { get; set; }
        public bool Status { get; set; }        
        public virtual Usuario UsuarioEnviou { get; set; }
        public virtual Usuario UsuarioRecebeu { get; set; }

    }
}