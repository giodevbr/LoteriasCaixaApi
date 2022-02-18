using RestSharp;
using System.Net;
using Newtonsoft.Json;
using Loterias.Core.Dtos;
using Loterias.Util.Resources;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Ibge.Dtos;
using Loterias.Infra.Data.Rest.Ibge.Interfaces;

namespace Loterias.Infra.Data.Rest.Ibge.Services
{
    public class IbgeConsultaService : IIbgeConsultaService
    {
        private readonly IDomainNotification _notificacaoDeDominio;

        public IbgeConsultaService(IDomainNotification notificacaoDeDominio)
        {
            _notificacaoDeDominio = notificacaoDeDominio;
        }

        public async Task<List<UfDto>> ConsultarUfs()
        {
            var client = new RestClient(StringResources.UrlBaseIbge);
            var request = new RestRequest(StringResources.UrlUfIbge, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.FalhaNaComunicacaoComApi));

            if (_notificacaoDeDominio.HasNotifications())
                return new List<UfDto>();

            var retorno = JsonConvert.DeserializeObject<List<UfDto>>(response.Content);

            return retorno;
        }

        public async Task<List<MunicipioDto>> ConsultarMunicipiosPorUf(int codigoIbgeUf)
        {
            var client = new RestClient(StringResources.UrlBaseIbge);
            var request = new RestRequest(StringResources.FormatarResource(StringResources.UrlMunicipioIbge, codigoIbgeUf), Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.FalhaNaComunicacaoComApi));

            if (string.IsNullOrEmpty(response.Content) || response.Content == "[]" && !_notificacaoDeDominio.HasNotifications())
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.CodigoIbgeDaUfInvalido));

            if (_notificacaoDeDominio.HasNotifications())
                return new List<MunicipioDto>();

            var retorno = JsonConvert.DeserializeObject<List<MunicipioDto>>(response.Content);

            return retorno;
        }
    }
}
