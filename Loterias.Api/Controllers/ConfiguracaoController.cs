using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracaoController : BaseController
    {
        private readonly IIbgeService _ibgeService;

        public ConfiguracaoController(IDomainNotification notificacaoDeDominio,
                                      IIbgeService ibgeService) : base(notificacaoDeDominio)
        {
            _ibgeService = ibgeService;
        }

        [HttpGet("ExecutarCargaDeUf")]
        public async Task<IActionResult> ExecutarCargaDeUf()
        {
            await _ibgeService.ExecutarCargaDeUf();

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok();
        }

        [HttpGet("ExecutarCargaDeMunicipio")]
        public async Task<IActionResult> ExecutarCargaDeMunicipio()
        {
            await _ibgeService.ExecutarCargaDeMunicipio();

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok();
        }
    }
}
