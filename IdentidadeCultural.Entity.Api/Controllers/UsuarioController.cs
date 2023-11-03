using IdentidadeCultural.Entity.Aplicacao.Service;
using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentidadeCultural.Entity.Api.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;

        public UsuarioController(
       //   ILogger logger,
       IUsuarioService service)
        {
            //    _logger = logger;
            _service = service;
        }

        
        /// <summary>
        /// Endpoint responsável 
        /// </summary>
        /// <returns>Retorna lista </returns>
        [HttpGet("api/usuario/buscarPorEmail")]
        [SwaggerOperation(Tags = new[] { " Usuario " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> BuscarPorEmail([FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return Ok();
                }
                var resposta = _service.BuscarPorEmail(email);

                if (resposta != null)//resposta.Status == 200)
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
        [HttpPost("api/usuarios/logar")]
        [SwaggerOperation(Tags = new[] { " Usuario " })]
        //[Authorize]
        [ProducesResponseType(typeof(Resposta<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Resposta<>), StatusCodes.Status500InternalServerError)]
        public ActionResult<Resposta<dynamic>> Login([FromBody] Login login)
        {
            try
            {
                
                var resposta = _service.Login(login);

                if (resposta.Status == 200)
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
    }

}

