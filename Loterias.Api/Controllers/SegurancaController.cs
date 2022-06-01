using Loterias.Api.Extensions;
using Loterias.Core.Interfaces;

namespace Loterias.Api.Controllers
{
    public class SegurancaController : BaseController
    {
        public SegurancaController(IDomainNotification notificacaoDeDominio) : base(notificacaoDeDominio)
        {
        }
    }
}
