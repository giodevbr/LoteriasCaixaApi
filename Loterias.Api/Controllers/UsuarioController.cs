using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Domain.Dtos;
using Loterias.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IDomainNotification notificacaoDeDominio,
                                    IUsuarioService usuarioService) : base(notificacaoDeDominio)
        {
            _usuarioService = usuarioService;
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
