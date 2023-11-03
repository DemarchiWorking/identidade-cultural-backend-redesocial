using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Entity.Dominio.Model.DTOs;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Aplicacao.Service
{
    public interface IAmizadeService
    {
        /*Resposta<dynamic> CadastrarUsuario(Usuario usuario);
        Resposta<dynamic> Login(Login login);
        UsuarioToken GerarToken(Login login);*/
        Resposta<Amizade> BuscarPorAmizadesPorId(Guid idUsuario, int? pagina);
        Resposta<Amizade> BuscarConvitesEmAberto(IdComPaginacao pagina);
        Resposta<Amizade> ConvidarAmizade(ConvidarRequest convidar);
        Resposta<bool> AceitarConvite(Guid amizadeId);
    }
}
