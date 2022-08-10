using Loterias.Infra.Data.Rest.Caixa.Dtos;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Loterias.Util.Resources;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Loterias.Infra.Data.Rest.Caixa.Services
{
    public class LotoFacilCaixaApiService : ILotoFacilCaixaApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _urlBaseCaixaApi;

        public LotoFacilCaixaApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _urlBaseCaixaApi = _configuration.GetSection("Apis:UrlBaseCaixaApi").Value;
        }

        public async Task<LotofacilDto> ObterResultadoPorConcurso(string concurso)
        {
            var client = new RestClient();
            var requestUrl = _urlBaseCaixaApi + StringResources.Lotofacil + "/" + concurso;
            var request = new RestRequest(requestUrl, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK || response.Content == null)
                return new LotofacilDto();

            var retorno = JsonConvert.DeserializeObject<LotofacilDto>(response.Content);

            if (retorno == null)
                return new LotofacilDto();

            return retorno;
        }
    }
}
