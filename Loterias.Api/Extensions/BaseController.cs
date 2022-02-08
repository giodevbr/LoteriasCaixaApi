using Loterias.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.Api.Extensions
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected readonly IDomainNotification _notificacaoDeDominio;

        protected BaseController(IDomainNotification notificacaoDeDominio)
        {
            _notificacaoDeDominio = notificacaoDeDominio;
        }

        protected bool OperacaoValida() => !_notificacaoDeDominio.HasNotifications();

        protected BadRequestObjectResult BadRequestResponse()
        {
            return BadRequest(_notificacaoDeDominio.GetNotifications().Select(n => n.Value).Distinct());
        }
    }
}
