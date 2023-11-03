using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using IdentidadeCultural.Entity.Aplicacao.Service;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model;
using Microsoft.AspNetCore.Authorization;

namespace IdentidadeCultural.Entity.Api.Controllers
{


    public class ServicoController : Controller
    {
        //private readonly ILogger _logger;
        private readonly IServicoTrabalhoService _service;

        public ServicoController(
        //   ILogger logger,
           IServicoTrabalhoService service)
        {
            //    _logger = logger;
            _service = service;
        }
        /// <summary>
        /// Endpoint responsável 
        /// </summary>
        /// <returns>Retorna lista </returns>
        [HttpGet("api/servicos")]
        [SwaggerOperation(Tags = new[] { " Serviços " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> ListaServicos()
        {
            try
            {
                FiltroServico f = new FiltroServico();
                var resposta = _service.ListarServicos(f);

                /* if (resposta.Status == 204)
                {
                    return NoContent();
                }*/

                if (resposta.Sucesso == true)
                {
                    return Ok(resposta.Dados);
                }
                else
                {
                    return BadRequest(resposta);
                }
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"[TemplateScreenController] Fatal error on GetAllScreens!");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Resposta<dynamic>()
            {
                Status = 500,
                Titulo = "Internal server error!",
                Sucesso = false
            });
        }

        /// <summary>
        /// Endpoint responsável 
        /// </summary>
        /// <returns>Retorna lista </returns>
        [HttpGet("api/servicos/por")]
        [SwaggerOperation(Tags = new[] { " Serviços " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> BuscarPorId([FromQuery] Guid idServico)
        {
            try
            {
                var resposta = _service.BuscarPorId(idServico);

                if (resposta.Status == 204)
                {
                    return NoContent();
                }

                if (resposta.Sucesso == true && resposta.Dados[0] != null)
                {
                    return Ok(resposta.Dados[0]);
                }

                else
                {
                    return BadRequest(resposta);
                }
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"[TemplateScreenController] Fatal error on GetAllScreens!");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Resposta<dynamic>()
            {
                Status = 500,
                Titulo = "Internal server error!",
                Sucesso = false
            });
        }




        /// <summary>
        /// Endpoint responsável 
        /// </summary>
        /// <returns>Retorna lista </returns>
        [HttpGet("api/servicoss")]
        [SwaggerOperation(Tags = new[] { " Serviços " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> ListaServicos([FromQuery] FiltroServico filtroQuery)
        {
            try
            {
                var resposta = _service.ListarServicos(filtroQuery);

                if (resposta.Status == 204)
                {
                    return NoContent();
                }

                if (resposta.Sucesso == true)
                {
                    return Ok(resposta);
                }

                else
                {
                    return BadRequest(resposta);
                }
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"[TemplateScreenController] Fatal error on GetAllScreens!");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Resposta<dynamic>()
            {
                Status = 500,
                Titulo = "Internal server error!",
                Sucesso = false
            });
        }


        /// <summary>
        /// Endpoint responsável 
        /// </summary>
        /// <returns>Retorna lista </returns>
        [HttpDelete("api/servicos")]
        [SwaggerOperation(Tags = new[] { " Serviços " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> ExcluirServicos([FromQuery] Guid idServico)
        {
            try
            {


                var resposta = _service.ExcluirServico(idServico);

                if (resposta.Status == 204)
                {
                    return NoContent();
                }
                if (resposta.Sucesso == true)
                {
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest(resposta);
                }

            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"[TemplateScreenController] Fatal error on GetAllScreens!");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Resposta<dynamic>()
            {
                Status = 500,
                Titulo = "Internal server error!",
                Sucesso = false
            });
        }
        /// <summary>
        /// Endpoint responsável 
        /// </summary>
        /// <returns>Retorna lista </returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("api/servicos")]
        [SwaggerOperation(Tags = new[] { " Serviços " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<ServicoTrabalhoReposta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<ServicoTrabalhoReposta>> AdicionarServico([FromBody] ServicoTrabalho servico)
        {
            try
            {
                var resposta = _service.AdicionarServico(servico);

                if (resposta.Status == 200)
                {
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest(resposta);
                }
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"[TemplateScreenController] Fatal error on GetAllScreens!");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Resposta<dynamic>()
            {
                Status = 500,
                Titulo = "Internal server error!",
                Sucesso = false
            });
        }


        /// <summary>
        /// Endpoint responsável 
        /// </summary>
        /// <returns>Retorna lista </returns>
        [HttpPut("api/servicos")]
        [SwaggerOperation(Tags = new[] { " Serviços " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<ServicoTrabalhoReposta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<ServicoTrabalhoReposta>> AtualizarServico([FromQuery] Guid idServico, [FromBody] ServicoTrabalho servico)
        {
            try
            {
                var resposta = _service.AtualizarServico(idServico, servico);

                if (resposta.Status == 200)
                {
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest(resposta);
                }
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"[TemplateScreenController] Fatal error on GetAllScreens!");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Resposta<dynamic>()
            {
                Status = 500,
                Titulo = "Internal server error!",
                Sucesso = false
            });
        }
    }
}
