using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model.Request
{
    public class IdComPaginacao
    {
        public Guid Id { get; set; }
        public int? Pagina { get; set; }
        public int? TamanhoPagina { get; set; }

    }
}