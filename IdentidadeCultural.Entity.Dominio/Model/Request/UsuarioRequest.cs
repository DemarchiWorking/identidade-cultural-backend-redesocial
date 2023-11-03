using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model.Request
{
    public class UsuarioRequest
    {

        public Guid? UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Titulo { get; set; }
        public string Telefone { get; set; }
        public string Foto { get; set; }
        public DateTime Criacao { get; set; }
        //public virtual ICollection<ServicoTrabalhoReposta> Servicos { get; set; }
        //public virtual ICollection<Produto> Produtos { get; set; }

    }
}