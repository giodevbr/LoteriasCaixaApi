using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotoFacilController : BaseController
    {
        private readonly ILotoFacilService _lotoFacilService;

        public LotoFacilController(IDomainNotification notificacaoDeDominio,
                                   ILotoFacilService lotoFacilService) : base(notificacaoDeDominio)
        {
            _lotoFacilService = lotoFacilService;
        }

        [HttpGet("ConsultarPlanilhaTodos")]
        public async Task<IActionResult> ConsultarPlanilhaTodos()
        {
            var retorno = await _lotoFacilService.ConsultarPlanilhaTodos();

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }

        [HttpGet("ConsultarPlanilhaPorConcurso")]
        public async Task<ActionResult> ConsultarPlanilhaPorConcurso(string concurso)
        {
            var retorno = await _lotoFacilService.ConsultarPlanilhaPorConcurso(concurso);

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }
    }
}
