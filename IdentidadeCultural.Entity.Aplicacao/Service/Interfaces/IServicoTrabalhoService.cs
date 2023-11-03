using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Aplicacao.Service
{
    public interface IServicoTrabalhoService
    {
         Resposta<ServicoTrabalhoReposta> ListarServicos(FiltroServico filtroServicoQuery);
         Resposta<ServicoTrabalhoReposta> BuscarPorId(Guid idServico);
         Resposta<ServicoTrabalhoReposta> AdicionarServico(ServicoTrabalho servico); 
        //Resposta<dynamic> AdicionarUsuario(Usuario usuario);
        //Resposta<dynamic> Login(Login login);
        
        Resposta<dynamic> ExcluirServico(Guid idServico);
        Resposta<dynamic> AtualizarServico(Guid idServico, ServicoTrabalho servico);
    }
}
