using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Dtos;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Loterias.Infra.Data.Rest.Caixa.Services
{
    public class UltimosResultadosService : IUltimosResultadosService
    {
        private readonly IConfiguration _configuration;
        private readonly string _urlPortalDeLoteriasUltimosResultados;

        public UltimosResultadosService(IConfiguration configuration)
        {
            _configuration = configuration;
            _urlPortalDeLoteriasUltimosResultados = _configuration.GetSection("Apis:UrlPortalDeLoteriasUltimosResultados").Value;
        }

        public async Task<UltimosResultadosDto> ConsultarUltimosResultados()
        {
            var client = new RestClient();
            var request = new RestRequest(_urlPortalDeLoteriasUltimosResultados, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK ||
                response.Content == string.Empty ||
                response.Content == null)
                return new UltimosResultadosDto();

            var retorno = JsonConvert.DeserializeObject<UltimosResultadosDto>(response.Content);

            if(retorno == null)
                return new UltimosResultadosDto();

            return retorno;
        }
    }
}
