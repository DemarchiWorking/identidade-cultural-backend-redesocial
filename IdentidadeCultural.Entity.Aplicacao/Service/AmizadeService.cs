using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Entity.Dominio.Model.DTOs;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using IdentidadeCultural.Entity.Infraestrutura;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace IdentidadeCultural.Entity.Aplicacao.Service
{
    public class AmizadeService : IAmizadeService
    {
        private readonly IdentityContext _context;
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;
        //private readonly ITemplateRepository _templateRepository;
        private readonly IHttpContextAccessor _accessor;
        public AmizadeService(
            IdentityContext context,
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
           //ILogger logger
           //, ITemplateRepository templateRepository
           IHttpContextAccessor accessor
           )
        {
            _context = context;
            _configuration = configuration;
            _accessor = accessor;
            //_logger = logger;
            //_templateRepository = templateRepository;
        }

        public Resposta<Amizade> BuscarPorAmizadesPorId(Guid idUsuario, int? pagina)
        {
            try
            {
                int paginaNumero = (pagina ?? 1);

                var query = _context.Amizades.Select(p => new Amizade()
                {
                    AmizadeId = p.AmizadeId,
                    UsuarioEnviouId = p.UsuarioEnviouId,
                    UsuarioEnviou = p.UsuarioEnviou,
                    UsuarioRecebeu = p.UsuarioRecebeu,
                    UsuarioRecebeuId = p.UsuarioRecebeuId,
                    Status = p.Status
                })
                   .Where(x=> x.Status == true && x.UsuarioRecebeuId == idUsuario || x.UsuarioEnviouId == idUsuario)
                   .OrderBy(f => f.AmizadeId).Skip((paginaNumero - 1) * 10).Take(10)
                   .ToList();

                if (query != null)
                {
                    return new Resposta<Amizade>()
                    {
                        Titulo = "Amizades encontradas com sucesso.",
                        Dados = query,
                        Status = 200,
                        Sucesso = true
                    };

                }
                else
                {
                    return new Resposta<Amizade>()
                    {
                        Titulo = "Erro ao cadastradar usuário.",
                        Dados = null,
                        Status = 400,
                        Sucesso = false
                    };

                }
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return new Resposta<Amizade>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };
        }
        public Usuario BuscarUsuarioPorEmail(string email)
        {
            try
            {
                var resposta = _context.Usuarios.FirstOrDefault(x => x.Email == email);

                if (resposta != null)
                {
                    if (resposta.Servicos == null)
                    {
                        resposta.Servicos = new List<ServicoTrabalho>();
                    }
                    if (resposta.Produtos == null)
                    {
                        resposta.Produtos = new List<Produto>();
                    }
                    return resposta;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                
            }
            return null;
        }
        public Resposta<Amizade> BuscarConvitesEmAberto(IdComPaginacao pagina)
        {
            try
            {
                var autoriza = _accessor.HttpContext.Request.Headers["Authorization"].ToString();
                if (autoriza != null)
                {
                   var tokenString = autoriza.Replace("Bearer ", "");
                   var tokenDecript = new JwtSecurityToken(jwtEncodedString: tokenString);
                   var email = tokenDecript.Payload["unique_name"];

                    if (email != null)
                    {
                     //   var usuario = this.BuscarUsuarioPorEmail(email.ToString());
                    int paginaNumero = (pagina.Pagina ?? 1);
                    int paginaTamanho = (pagina.TamanhoPagina ?? 10);

                    var query = _context.Amizades.Select(p => new Amizade()
                    {
                        AmizadeId = p.AmizadeId,
                        UsuarioEnviouId = p.UsuarioEnviouId,
                        UsuarioEnviou = p.UsuarioEnviou,
                        UsuarioRecebeu = p.UsuarioRecebeu,
                        UsuarioRecebeuId = p.UsuarioRecebeuId,
                        Status = p.Status
                    })
                       .Where(x => x.Status == false)
                       .Where(x => x.UsuarioRecebeu.Email == email.ToString())
                       .Skip((paginaNumero - 1) * paginaTamanho).Take(paginaTamanho)
                     .ToList();

                    if (query != null)
                    {
                        return new Resposta<Amizade>()
                        {
                            Titulo = "Amizades encontradas com sucesso.",
                            Dados = query,
                            Status = 200,
                            Sucesso = true
                        };
                        }
                    }
                }
                else
                {
                    return new Resposta<Amizade>()
                    {
                        Titulo = "Erro ao cadastradar usuário.",
                        Dados = null,
                        Status = 400,
                        Sucesso = false
                    };

                }
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return new Resposta<Amizade>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };
        }

        
        public Resposta<bool> AceitarConvite(Guid amizadeId)
        {
            try
            {
                var autoriza = _accessor.HttpContext.Request.Headers["Authorization"].ToString();
                if (autoriza != null)
                {
                    var tokenString = autoriza.Replace("Bearer ", "");
                    var tokenDecript = new JwtSecurityToken(jwtEncodedString: tokenString);
                    var email = tokenDecript.Payload["unique_name"];

                    if (email != null)
                    {
                        var query = _context.Amizades.Find(amizadeId);

                        if (query != null)
                        {
                            var usuario = this.BuscarUsuarioPorEmail(email.ToString());

                            if (usuario != null)
                            {
                                if (usuario.Email != email.ToString() || query.Status == true)
                                {
                                    return new Resposta<bool>()
                                    {
                                        Titulo = "Você já aceitou, ou precisa se autenticar com as credenciais necessárias.",
                                        Status = 400,
                                        Dados = new List<bool>() { false },
                                        Sucesso = false
                                    };
                                }
                                else
                                {
                                    query.Status = true;
                                    var resultado = _context.SaveChanges();
                                    if (resultado > 0)
                                    {
                                        return new Resposta<bool>()
                                        {
                                            Titulo = "Usuário aceito com sucesso.",
                                            Status = 200,
                                            Dados = new List<bool>() { true },
                                            Sucesso = true
                                        };
                                    }
                                    else
                                    {
                                        return new Resposta<bool>()
                                        {
                                            Titulo = "Amizade não foi atualizada",
                                            Status = 400,
                                            Dados = new List<bool>() { false },
                                            Sucesso = false
                                        };
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return new Resposta<bool>()
            {
                Titulo = "Erro 500.",
                Status = 500,
                Dados = null,
                Sucesso = false
            };
        }

        public Resposta<Amizade> ConvidarAmizade(ConvidarRequest convidar)
        {
            try
            {

                var amizade = new Amizade();
                amizade.UsuarioEnviouId = convidar.UsuarioEnviouId;
                amizade.UsuarioRecebeuId = convidar.UsuarioRecebeuId;

                var query = _context.Amizades.Select(p => new Amizade()
                {
                    AmizadeId = p.AmizadeId,
                    UsuarioEnviouId = p.UsuarioEnviouId,
                    UsuarioEnviou = p.UsuarioEnviou,
                    UsuarioRecebeu = p.UsuarioRecebeu,
                    UsuarioRecebeuId = p.UsuarioRecebeuId,
                    Status = p.Status
                })
                    .Where(x => x.UsuarioEnviouId == convidar.UsuarioEnviouId || x.UsuarioRecebeuId == convidar.UsuarioRecebeuId)
                    .ToList();

                List<Amizade> amizadeAdd = new List<Amizade>();

                if (query != null)
                {
                    if (query.Count() > 0)
                    {
                        return new Resposta<Amizade>()
                        {
                            Titulo = "Pedido de amizade já foi enviado",
                            Status = 200,
                            Dados = amizadeAdd,
                            Sucesso = true
                        };

                    }
                }
                _context.Amizades.Add(amizade);
                amizadeAdd.ToList<dynamic>().ForEach(it =>
                {
                    amizadeAdd.Add(amizade);
                });

                var resposta = _context.SaveChanges();
                if (resposta > 0)
                {

                    return new Resposta<Amizade>()
                    {
                        Titulo = "Pedido de amizade enviado com sucesso.",
                        Status = 200,
                        Dados = amizadeAdd,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Amizade>()
                    {
                        Titulo = "Não foi possível enviar o pedido de amizade.",
                        Status = 400,
                        Dados = null,
                        Sucesso = false
                    };
                }
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return new Resposta<Amizade>()
            {
                Titulo = "Erro 500.",
                Status = 500,
                Dados = null,
                Sucesso = false
            };
        }
    }
}
