using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Domain.Dtos;
using Loterias.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurancaController : BaseController
    {
        private readonly ISegurancaService _segurancaService;

        public SegurancaController(IDomainNotification notificacaoDeDominio,
                                   ISegurancaService segurancaService) : base(notificacaoDeDominio)
        {
            _segurancaService = segurancaService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var usuario = await _segurancaService.Login(loginDto);

            if (_notificacaoDeDominio.HasNotifications() || usuario == null)
                return BadRequestResponse();

            var token = _segurancaService.GerarToken(usuario);

            return Ok(token);
        }
    }
}
