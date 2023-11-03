using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model.Request
{
    public  class FiltroServico
    {

        //public int Pagina { get; set; }
        //public int TamanhoPagina { get; set; }
        public Guid? Id { get; set; }
        public Guid? UsuarioId { get; set; }
        public string? Titulo { get; set; }
        public string? Categoria { get; set; }
    }
}
