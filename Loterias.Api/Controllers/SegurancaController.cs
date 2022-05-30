using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Domain.Dtos;
using Loterias.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    public class SegurancaController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public SegurancaController(IDomainNotification notificacaoDeDominio, 
                                   IUsuarioService usuarioService) : base(notificacaoDeDominio)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            await _usuarioService.ValidarLogin(loginDto);

            if (!_notificacaoDeDominio.HasNotifications())
            {
                var tokenString = GerarTokenJWT();
                return Ok(new { token = tokenString });
            }
            else
            {
                return BadRequestResponse();
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(UsuarioDto usuarioDto)
        {
            await _usuarioService.Cadastrar(usuarioDto);

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok();
        }
    }
}
