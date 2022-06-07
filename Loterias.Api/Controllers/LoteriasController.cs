using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoteriasController : BaseController
    {
        private readonly IUltimosResultadosService _ultimosResultadosService;

        public LoteriasController(IDomainNotification notificacaoDeDominio,
                                  IUltimosResultadosService ultimosResultadosService) : base(notificacaoDeDominio)
        {
            _ultimosResultadosService = ultimosResultadosService;
        }

        [HttpGet("ConsultarUltimosResultados")]
        public async Task<IActionResult> ConsultarUltimosResultados()
        {
            var retorno = await _ultimosResultadosService.ConsultarUltimosResultados();

            if (_notificacaoDeDominio.HasNotifications())
                return BadRequestResponse();

            return Ok(retorno);
        }
    }
}
