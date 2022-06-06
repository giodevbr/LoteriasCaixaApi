using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Ibge.Dtos;
using Loterias.Infra.Data.Rest.Ibge.Interfaces;
using Loterias.Util.Resources;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Loterias.Infra.Data.Rest.Ibge.Services
{
    public class IbgeApiService : IIbgeApiService
    {
        private readonly IDomainNotification _notificacaoDeDominio;
        private readonly IConfiguration _configuration;
        private readonly string _urlBaseIbge;

        public IbgeApiService(IDomainNotification notificacaoDeDominio, IConfiguration configuration)
        {
            _notificacaoDeDominio = notificacaoDeDominio;
            _configuration = configuration;
            _urlBaseIbge = _configuration.GetSection("Apis:UrlBaseIbge").Value;
        }

        public async Task<List<UfDto>> ConsultarUfs()
        {
            var client = new RestClient(_urlBaseIbge);
            var request = new RestRequest(StringResources.UrlUfIbge, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                _notificacaoDeDominio.AddNotification(StringResources.FalhaNaComunicacaoComApi);

            if (_notificacaoDeDominio.HasNotifications() || response.Content == null)
                return new List<UfDto>();

            var retorno = JsonConvert.DeserializeObject<List<UfDto>>(response.Content);

            if (retorno == null)
                return new List<UfDto>();

            return retorno;
        }

        public async Task<List<MunicipioDto>> ConsultarMunicipiosPorUf(int codigoIbgeUf)
        {
            var client = new RestClient(_urlBaseIbge);
            var request = new RestRequest(StringResources.FormatarResource(StringResources.UrlMunicipioIbge, codigoIbgeUf), Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                _notificacaoDeDominio.AddNotification(StringResources.FalhaNaComunicacaoComApi);

            if (string.IsNullOrEmpty(response.Content) || response.Content == "[]" && !_notificacaoDeDominio.HasNotifications())
                _notificacaoDeDominio.AddNotification(StringResources.CodigoIbgeDaUfInvalido);

            if (_notificacaoDeDominio.HasNotifications() || response.Content == null) 
                return new List<MunicipioDto>();

            var retorno = JsonConvert.DeserializeObject<List<MunicipioDto>>(response.Content);

            if(retorno == null)
                return new List<MunicipioDto>();

            return retorno;
        }
    }
}
