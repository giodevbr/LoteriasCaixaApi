using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    public class LotoFacilController : BaseController
    {
        private readonly ILotoFacilConsultaService _lotoFacilConsultaService;

        public LotoFacilController(IDomainNotification notificacaoDeDominio,
                                   ILotoFacilConsultaService lotoFacilConsultaService) : base(notificacaoDeDominio)
        {
            _lotoFacilConsultaService = lotoFacilConsultaService;
        }

        [HttpGet("Consultar")]
        public async Task<IActionResult> Consultar()
        {
            var retorno = await _lotoFacilConsultaService.Consultar();

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }

        [HttpGet("ConsultarUltimoConcurso")]
        public async Task<IActionResult> ConsultarUltimoConcurso()
        {
            var retorno = await _lotoFacilConsultaService.ConsultarUltimoConcurso();

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }

        [HttpGet("ConsultarPorConcurso")]
        public async Task<ActionResult> ConsultarPorConcurso(string concurso)
        {
            var retorno = await _lotoFacilConsultaService.ConsultarPorConcurso(concurso);

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }

        [HttpGet("ConsultarPorDataDoSorteio")]
        public async Task<IActionResult> ConsultarPorDataDoSorteio(DateTime dataDoSorteio)
        {
            var retorno = await _lotoFacilConsultaService.ConsultarPorDataDoSorteio(dataDoSorteio);

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }
    }
}
