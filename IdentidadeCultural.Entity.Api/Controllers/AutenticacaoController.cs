using IdentidadeCultural.Entity.Aplicacao.Service;
using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Entity.Dominio.Model.Request;
using IdentidadeCultural.Entity.Dominio.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentidadeCultural.Entity.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AutenticacaoController : Controller
    {
        private readonly IUsuarioService _service;


        private readonly UserManager<IdentityUser> _gerenciadorUsuario;
        private readonly SignInManager<IdentityUser> _gerenciadorRegistro;

        public AutenticacaoController(UserManager<IdentityUser> gerenciadorUsuario, SignInManager<IdentityUser> gerenciadorRegistro, IUsuarioService service)
        {
            _gerenciadorUsuario = gerenciadorUsuario;
            _gerenciadorRegistro = gerenciadorRegistro;
            _service = service;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Bateu";
        }

        [HttpPost("cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult> CadastrarUsuario([FromBody]Usuario usuario)
        {
            
            var identityUser = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true
            };
            var resultado = await _gerenciadorUsuario.CreateAsync(identityUser, usuario.Senha);

            

            if (!resultado.Succeeded)
            {
                return BadRequest(resultado.Errors);
            }

            _service.CadastrarUsuario(usuario);
            await _gerenciadorRegistro.SignInAsync(identityUser, false);
            var login = new Login();
            login.Email = usuario.Email;
            login.Senha = usuario.Senha;

            return Ok(_service.GerarToken(login));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            var resultado = await _gerenciadorRegistro.PasswordSignInAsync(login.Email, login.Senha, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {

                return Ok(_service.GerarToken(login));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido...");
                return BadRequest(ModelState);
            }
        }

    }
}
