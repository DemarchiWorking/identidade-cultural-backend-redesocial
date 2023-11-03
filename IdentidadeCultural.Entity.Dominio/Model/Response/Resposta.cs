using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model.Response
{
    public class Resposta<T>
    {
        public string Titulo { get; set; }
        public bool Sucesso { get; set; }
        public int Status { get; set; }
        public List<T> Dados { get; set; }
        //public bool Vazio => Dados.Count() == 0 ? true : false;    
    }
}
