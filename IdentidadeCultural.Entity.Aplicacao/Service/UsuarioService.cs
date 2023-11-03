using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Entity.Dominio.Model.DTOs;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using IdentidadeCultural.Entity.Infraestrutura;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly IdentityContext _context;
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;
        //private readonly ITemplateRepository _templateRepository;

        public UsuarioService(
            IdentityContext context,
            IConfiguration configuration
            //ILogger logger
           //, ITemplateRepository templateRepository
           )
        {
            _context = context;
            _configuration = configuration;
            //_logger = logger;
            //_templateRepository = templateRepository;
        }


    public Resposta<dynamic> CadastrarUsuario(Usuario usuario)
    {
        try
        {
            _context.Usuarios.Add(usuario);

            var resposta = _context.SaveChanges();

            if(resposta > 0)
            {
                return new Resposta<dynamic>()
                {
                     Titulo = "Usuário cadastrado com sucesso.",    
                     Dados = new List<dynamic>() { usuario.UsuarioId, usuario.Nome, usuario.Email, usuario.Telefone },
                     Status = 200,
                     Sucesso = true
                };

            }
            else{
                return new Resposta<dynamic>()
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
        return new Resposta<dynamic>()
        {
                Titulo = "Erro 500.",    
                Dados = null,
                Status = 500,
                Sucesso = false
        };


    }
        public Usuario BuscarPorEmail(string email)
        {
            try
            {
                var resposta = _context.Usuarios.FirstOrDefault(x => x.Email == email);

                if (resposta != null)
                {
                    if(resposta.Servicos == null)
                    {
                        resposta.Servicos = new List<ServicoTrabalho>();
                    }
                    if(resposta.Produtos == null)
                    {
                        resposta.Produtos = new List<Produto>();
                    }
                   return resposta;                
                }
                else
                {
                    return null;
                    /*
                    return new Usuario()
                    {
                        Titulo = "Não foi possível encontrar usuário.",
                        Dados = null,
                        Status = 400,
                        Sucesso = false
                    };
                    */
                }
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return null;
                /*new Resposta<dynamic>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };*/
        }
        public Resposta<dynamic> Login(Login login)
        {
            try
            {
                var solicitacao = _context.Usuarios;

                var query = solicitacao.Select(p => new UsuarioResposta()
                {
                    UsuarioId = p.UsuarioId,
                    Nome = p.Nome,
                    Email = p.Email,
                    Senha = p.Senha,
                    Titulo = p.Titulo,
                    Telefone = p.Telefone,
                    Foto = p.Foto
                })
                    .Where(x => (x.Email == login.Email) && (x.Senha == login.Senha))
                    .ToList();


                if (query == null)
                {   
                    //if(query.Count == 0)
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Não foi possível encontrar o usuário.",
                        Status = 400,
                        Dados = null,
                        Sucesso = false
                    };

                }
                if (query.Count() == 0)
                {
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Usuário ou senha inválido.",
                        Status = 204,
                        Dados = null,
                        Sucesso = true
                    };
                }
                else
                {
                    var resposta = query.FirstOrDefault();
                    if(resposta != null) resposta.Senha = "****";

                    return new Resposta<dynamic>()
                    {
                        Titulo = "Usuário encontrados com sucesso.",
                        Status = 200,
                        Dados = new List<dynamic>() { resposta },
                        Sucesso = true
                    };
                }
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return new Resposta<dynamic>()
            {
                Titulo = "Erro 500",
                Status = 500,
                Dados = null,
                Sucesso = false
            };
        }

        public UsuarioToken GerarToken(Login login)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, login.Email),
                new Claim("role","Usuario"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            return new UsuarioToken()
            {
                Autenticado = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracao = expiration,
                Mensagem = "Token JWT: Sucesso.",
                Usuario = login.Email
            };


        }
    }       
}
