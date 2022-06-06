using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Ibge.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IbgeController : BaseController
    {
        private readonly IIbgeApiService _ibgeApiService;

        public IbgeController(IDomainNotification notificacaoDeDominio,
                              IIbgeApiService ibgeApiService)
                             : base(notificacaoDeDominio)
        {
            _ibgeApiService = ibgeApiService;
        }

        [HttpGet("ConsultarUfs")]
        public async Task<IActionResult> ConsultarUfs()
        {
            var retorno = await _ibgeApiService.ConsultarUfs();

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }

        [HttpGet("ConsultarMunicipiosPorUf")]
        public async Task<IActionResult> ConsultarMunicipiosPorUf(int codigoIbgeUf)
        {
            var retorno = await _ibgeApiService.ConsultarMunicipiosPorUf(codigoIbgeUf);

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }
    }
}
