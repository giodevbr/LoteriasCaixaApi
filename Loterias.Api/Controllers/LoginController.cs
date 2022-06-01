using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Domain.Dtos;
using Loterias.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;

        public LoginController(IDomainNotification notificacaoDeDominio,
                                  ILoginService loginService) : base(notificacaoDeDominio)
        {
            _loginService = loginService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var usuario = await _loginService.Login(loginDto);

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            #pragma warning disable CS8604 // Possível argumento de referência nula.
            var token = _loginService.GerarToken(usuario);
            #pragma warning restore CS8604 // Possível argumento de referência nula.

            return Ok(token);
        }
    }
}
