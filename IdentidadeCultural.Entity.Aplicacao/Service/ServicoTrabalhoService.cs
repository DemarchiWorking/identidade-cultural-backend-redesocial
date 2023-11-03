using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using IdentidadeCultural.Entity.Infraestrutura;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Aplicacao.Service
{
    public class ServicoTrabalhoService : IServicoTrabalhoService
    {
        private readonly IdentityContext _context;
        //private readonly ILogger _logger;
        //private readonly ITemplateRepository _templateRepository;

        public ServicoTrabalhoService(
            IdentityContext context
            //ILogger logger
           //, ITemplateRepository templateRepository
           )
        {
            _context = context;
            //_logger = logger;
            //_templateRepository = templateRepository;
        }

    public Resposta<ServicoTrabalhoReposta> ListarServicos(FiltroServico filtroServicoQuery)
    {
       try
       {
           var solicitacao = _context.Servicos;
            

            var query = solicitacao.Select(p => new ServicoTrabalhoReposta()
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Categoria = p.Categoria,
                    Descricao = p.Descricao,
                    Foto = p.Foto,
                    Criacao = p.Criacao,
                    UsuarioId = p.UsuarioId,
                    Usuario = p.Usuario
                }).ToList();
           
           /*
           if(!string.IsNullOrEmpty(filtroServicoQuery.Categoria))
           {
              query = solicitacao.Where(x => x.Categoria.Contains(filtroServicoQuery.Categoria));
           }
           if(!string.IsNullOrEmpty(filtroServicoQuery.Titulo))
           {
              query = solicitacao.Where(x => x.Titulo == filtroServicoQuery.Titulo);
           }           
           if(filtroServicoQuery.Usuario_Id != null)
           {
              query = solicitacao.Where(x => x.Usuario_Id == filtroServicoQuery.Usuario_Id);
           }
           if(filtroServicoQuery.Id != null)
           {
              query = solicitacao.Where(x => x.Id == filtroServicoQuery.Id);
           }
                
                */
                   //.Select(p => p.Usuario_Id != null);
                   //.Where(x=> x.Id == idUsuario)

           if (query == null)
           {
              return new Resposta<ServicoTrabalhoReposta>()
              {
                  Titulo = "Nenhum serviço disponível.",
                  Status = 400,
                  Dados = null,
                  Sucesso = false
              };
                
           }
           /*
           if(solicitacao.Count() == 0)
           {
                return new Resposta<ServicoTrabalhoReposta>()
              {
                  Titulo = "Nenhum serviço foi encontrado.",
                  Status = 204,
                  Dados = null,
                  Sucesso = true
              };
           }*/
           else
           {
              return new Resposta<ServicoTrabalhoReposta>()
              {
                  Titulo = "Serviços encontrados com sucesso.",
                  Status = 200,
                  Dados = query,
                  Sucesso = true
              };
           }
       }
       catch (Exception e)
       {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
       }
    return new Resposta<ServicoTrabalhoReposta>()
      {
         Titulo = "Erro 500",
         Status = 500,
         Dados = null,
         Sucesso = false
      };
    }

    
    public Resposta<ServicoTrabalhoReposta> BuscarPorId(Guid idServico)
    {
       try
       {
           var servico = _context.Servicos.Find(idServico);
           
           if (servico != null)
           {
              var resposta = new List<ServicoTrabalhoReposta>()
              {
                new ServicoTrabalhoReposta()
                {
                    Id = idServico,
                    Titulo = servico.Titulo,
                    Categoria = servico.Categoria,
                    Descricao = servico.Descricao,
                    Foto = servico.Foto,
                    Criacao = servico.Criacao,
                    Usuario = servico.Usuario,
                    UsuarioId = servico.UsuarioId
                }
              };

              return new Resposta<ServicoTrabalhoReposta>()
              {
                  Titulo = "Serviços encontrados com sucesso.",
                  Status = 200,
                  Dados = resposta,
                  Sucesso = true
              };
           }
           else
           {
              return new Resposta<ServicoTrabalhoReposta>()
              {
                  Titulo = "Nenhum serviço disponível.",
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
    return new Resposta<ServicoTrabalhoReposta>()
      {
         Titulo = "Erro 500",
         Status = 500,
         Dados = null,
         Sucesso = false
      };
    }

    public Resposta<ServicoTrabalhoReposta> AdicionarServico(ServicoTrabalho servico)
    {
        try
        {
            var solicitacao = _context.Servicos.Add(servico);
            List<ServicoTrabalhoReposta> servicoAdd = new List<ServicoTrabalhoReposta>();
        
            servicoAdd.ToList<dynamic>().ForEach(it =>
                        {
                            servicoAdd.Add(new ServicoTrabalhoReposta
                            {
                                Id = servico.Id,    
                                Titulo = servico.Titulo,
                                Categoria = servico.Categoria,
                                Descricao = servico.Descricao,
                                Foto = servico.Foto,
                                Criacao = servico.Criacao,
                                UsuarioId = servico.UsuarioId,
                                Usuario = servico.Usuario
                            }); 
                        });

            var resposta = _context.SaveChanges();
            if(resposta > 0){
           
               return new Resposta<ServicoTrabalhoReposta>()
               {
                   Titulo = "Serviço cadastrado com sucesso.",
                   Status = 200,
                   Dados = servicoAdd,
                   Sucesso = true
               };           
            }
            else
            {
               return new Resposta<ServicoTrabalhoReposta>()
               {
                  Titulo = "Não foi possível cadastrar esse usuário",
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
      return new Resposta<ServicoTrabalhoReposta>()
      {
         Titulo = "Erro 500.",
         Status = 500,
         Dados = null,
         Sucesso = false
      };
    }
    
    public Resposta<dynamic> ExcluirServico(Guid idServico)
    {
         try{
             var servico = _context.Servicos.Find(idServico);

            if(servico == null) 
            { 
                return new Resposta<dynamic>()
                {
                    Titulo = "Não existe serviço com esse Id",    
                    Dados = new List<dynamic>() { idServico },
                    Status = 204,
                    Sucesso = true
                };

            }
            else
            {
                _context.Servicos.Remove(servico);
                var resposta = _context.SaveChanges();

                if(resposta > 0) 
                { 
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Usuário excluído com sucesso.",    
                        Dados = new List<dynamic>() { servico },
                        Status = 200,
                        Sucesso = true
                    };
                }
                else{
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Não foi possível excluir esse serviço",    
                        Dados = new List<dynamic>() { idServico },
                        Status = 400,
                        Sucesso = false
                    };
                }
            }
         }
         catch {

         }        
         return new Resposta<dynamic>()
         {
             Titulo = "Erro 500.",    
             Dados = null,
             Status = 500,
             Sucesso = false
         };
    }

    public Resposta<dynamic> AdicionarUsuario(Usuario usuario)
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
                     Titulo = "Usuário cadastrado com sucesso.",    
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


        public Resposta<dynamic> AtualizarServico(Guid idServico, ServicoTrabalho servico)
        {
            try
            {
                var servicoAnterior = _context.Servicos.Find(idServico);

                if (servico == null)
                {
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Não existe serviço com esse Id",
                        Dados = new List<dynamic>() { servico },
                        Status = 204,
                        Sucesso = true
                    };

                }
                else
                {
                    servicoAnterior.Titulo = servico.Titulo;
                    servicoAnterior.Descricao = servico.Descricao;
                    servicoAnterior.Categoria = servico.Categoria;
                    servicoAnterior.Foto = servico.Foto;


                    var resposta = _context.SaveChanges();

                    if (resposta > 0)
                    {
                        return new Resposta<dynamic>()
                        {
                            Titulo = "Usuário alterado com sucesso.",
                            Dados = new List<dynamic>() { servico },
                            Status = 200,
                            Sucesso = true
                        };
                    }
                    else
                    {
                        return new Resposta<dynamic>()
                        {
                            Titulo = "Não foi possível alterar esse serviço",
                            Dados = new List<dynamic>() { servico },
                            Status = 400,
                            Sucesso = false
                        };
                    }
                }
            }
            catch
            {

            }
            return new Resposta<dynamic>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };
        }
    }       
}
