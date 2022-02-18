using Loterias.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Ibge.Interfaces;

namespace Loterias.Api.Controllers
{
    public class IbgeController : BaseController
    {
        private readonly IIbgeConsultaService _ibgeConsultaService;

        public IbgeController(IDomainNotification notificacaoDeDominio,
                              IIbgeConsultaService ibgeConsultaService) : base(notificacaoDeDominio)
        {
            _ibgeConsultaService = ibgeConsultaService;
        }


        [HttpGet("ConsultarUfs")]
        public async Task<IActionResult> Consultar()
        {
            var retorno = await _ibgeConsultaService.ConsultarUfs();

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }

        [HttpGet("ConsultarMunicipiosPorUf")]
        public async Task<IActionResult> ConsultarMunicipiosPorUf(int codigoIbgeUf)
        {
            var retorno = await _ibgeConsultaService.ConsultarMunicipiosPorUf(codigoIbgeUf);

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }
    }
}
