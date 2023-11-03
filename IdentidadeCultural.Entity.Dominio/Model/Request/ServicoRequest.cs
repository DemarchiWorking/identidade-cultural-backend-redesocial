using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model.Request
{
    public  class ServicoRequest
    {
        public Guid Id { get; set; }
        public Guid? UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public DateTime Criacao { get; set; }
    }
}
