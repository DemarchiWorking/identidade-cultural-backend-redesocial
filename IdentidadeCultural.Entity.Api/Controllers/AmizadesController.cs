using IdentidadeCultural.Entity.Aplicacao.Service;
using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentidadeCultural.Entity.Api.Controllers
{
    public class AmizadesController : Controller
    {
        private readonly IAmizadeService _service;

        public AmizadesController(
       //   ILogger logger,
       IAmizadeService service)
        {
            //    _logger = logger;
            _service = service;
        }


        /// <summary>
        /// Endpoint responsável 
        /// </summary>
        /// <returns>Retorna lista </returns>
        [HttpGet("api/amigo/porId")]
        [SwaggerOperation(Tags = new[] { " Usuario " })]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<Amizade>> BuscarAmizadePorId([FromQuery] IdComPaginacao idComPagina)
        {
            try
            {
                var resposta = _service.BuscarPorAmizadesPorId(idComPagina.Id, idComPagina.Pagina);

                if (resposta != null)//resposta.Status == 200)
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
        [HttpGet("api/convite/porId")]
        [SwaggerOperation(Tags = new[] { " Usuario " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> BuscarConvitesPorId([FromBody] IdComPaginacao pagina)
        {
            try
            {
                var resposta = _service.BuscarConvitesEmAberto(pagina);
                
                if (resposta != null)
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
        [HttpPost("api/amigo/convidar")]
        [SwaggerOperation(Tags = new[] { " Usuario " })]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> Convidar([FromBody] ConvidarRequest convidar)
        {
            try
            {
                var resposta = _service.ConvidarAmizade(convidar);

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
        [HttpPut("api/convite/aceitar")]
        [SwaggerOperation(Tags = new[] { " Usuario " })]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> AceitarConvite([FromBody] Guid amizadeId)
        {
            try
            {
                var resposta = _service.AceitarConvite(amizadeId);

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

