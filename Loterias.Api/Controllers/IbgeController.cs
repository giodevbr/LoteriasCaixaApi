using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Domain.Interfaces;
using Loterias.Infra.Data.Rest.Ibge.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    public class IbgeController : BaseController
    {
        private readonly IIbgeConsultaService _ibgeConsultaService;
        private readonly IIbgeService _ibgeService;

        public IbgeController(IDomainNotification notificacaoDeDominio,
                              IIbgeConsultaService ibgeConsultaService,
                              IIbgeService ibgeService)
                             : base(notificacaoDeDominio)
        {
            _ibgeConsultaService = ibgeConsultaService;
            _ibgeService = ibgeService;
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
