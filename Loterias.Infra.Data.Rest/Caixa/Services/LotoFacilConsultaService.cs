using RestSharp;
using System.Net;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Dtos;
using Loterias.Infra.Data.Rest.Caixa.Enums;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Microsoft.Extensions.Configuration;
using Loterias.Core.Dtos;

namespace Loterias.Infra.Data.Rest.Caixa.Services
{
    public class LotoFacilConsultaService : ILotoFacilConsultaService
    {
        private readonly IDomainNotification _notificacaoDeDominio;
        private readonly IConfiguration _configuration;
        private readonly string _urlBase;
        private readonly string _urlLotofacil;

        public LotoFacilConsultaService(IConfiguration configuration, 
                                        IDomainNotification notificacaoDeDominio)
        {
            _configuration = configuration;
            _notificacaoDeDominio = notificacaoDeDominio;
            _urlBase = _configuration.GetSection("EnderecosCaixa").GetSection("UrlBase").Value;
            _urlLotofacil = _configuration.GetSection("EnderecosCaixa").GetSection("UrlLotofacil").Value;
        }


        public async Task<List<LotofacilDto>> Consultar()
        {
            return await ConsultarCaixa();
        }

        public async Task<LotofacilDto> ConsultarUltimoConcurso()
        {
            var retorno = await ConsultarCaixa();
            var concursoRetorno = retorno.LastOrDefault();

            return concursoRetorno;
        }

        public async Task<LotofacilDto> ConsultarPorConcurso(string concurso)
        {
            var retorno = await ConsultarCaixa();
            var concursoRetorno = retorno.Where(c => c.Concurso == concurso).FirstOrDefault();

            return concursoRetorno;
        }

        public async Task<LotofacilDto> ConsultarPorDataDoSorteio(DateTime dataDoSorteio)
        {
            var retorno = await ConsultarCaixa();
            var concursoRetorno = retorno.Where(c => c.DataSorteio == dataDoSorteio).FirstOrDefault();

            return concursoRetorno;
        }

        private async Task<List<LotofacilDto>> ConsultarCaixa()
        {
            var client = new RestClient(_urlBase);
            var request = new RestRequest(_urlLotofacil, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK || 
                response.Content == string.Empty || 
                response.Content == null)
                return new List<LotofacilDto>();

            var retorno = TratarRetorno(response.Content);

            return retorno;
        }

        private List<LotofacilDto> TratarRetorno(string retorno)
        {
            var listaLotoFacil = new List<LotofacilDto>();

            try
            {
                var tabelaCaixa = TransformarTabelaHtmlEmArray(retorno);

                foreach (var linha in tabelaCaixa)
                {
                    var lotofacilDto = new LotofacilDto();
                    var arrayDeColunas = TransformarLinhaHtmlEmArray(linha);
                    var colunasCaixa = RemoverLinhasComLixoDoArrayDeColunas(arrayDeColunas);
                    var totalDeColunas = colunasCaixa.Length;

                    PreencherInformacoesDoSorteio(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherValoresPagos(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherGanhadores(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherResultados(lotofacilDto, colunasCaixa);
                    PreencherCidades(lotofacilDto);
                    PreencherStatus(lotofacilDto);

                    listaLotoFacil.Add(lotofacilDto);
                }
            }
            catch (Exception)
            {
                _notificacaoDeDominio.AddNotification(new Notification("Desculpe, ocorreu uma falha no processamento dos dados. Tente novamente mais tarde!"));
            }

            return listaLotoFacil;
        }

        private static string[] TransformarTabelaHtmlEmArray(string tabelaCaixa)
        {
            return tabelaCaixa.ToLower()
                              .Replace("<tr bgcolor=\"#b5ffbd\">", "<tr bgcolor=\"#ffffff\">")
                              .Split("<tr bgcolor=\"#ffffff\">")
                              .Where(x => !x.Contains("html"))
                              .ToArray();
        }

        private static string[] TransformarLinhaHtmlEmArray(string linhaTabelaCaixa)
        {
            return linhaTabelaCaixa.Replace("<td rowspan=\"1\">", "")
                                   .Replace("</td>", "")
                                   .Split('\n');
        }

        private static string[] RemoverLinhasComLixoDoArrayDeColunas(string[] colunasCaixa)
        {
            return colunasCaixa.Where(c => c != "" &&
                                                    c != " " &&
                                                    c != "</tr>" &&
                                                    c != null &&
                                                   !c.Contains("<td>\r") &&
                                                   !c.Contains("<tr>\r") &&
                                                   !c.Contains("table") &&
                                                   !c.Contains("cidade"))
                                                     .ToArray();
        }

        private static void PreencherInformacoesDoSorteio(LotofacilDto lotofacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotofacilDto.Concurso = colunasCaixa[0];
            lotofacilDto.DataSorteio = Convert.ToDateTime(colunasCaixa[1]);
            lotofacilDto.ValorArrecadado = Convert.ToDecimal(colunasCaixa[17]);
            lotofacilDto.ValorSorteado = Convert.ToDecimal(colunasCaixa[totalDeColunas - 2]);
            lotofacilDto.ValorAcumulado = Convert.ToDecimal(colunasCaixa[totalDeColunas - 3]);
        }

        private static void PreencherValoresPagos(LotofacilDto lotofacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotofacilDto.ValorPagoOnzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 4]);
            lotofacilDto.ValorPagoDozePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 5]);
            lotofacilDto.ValorPagoTrezePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 6]);
            lotofacilDto.ValorPagoQuatorzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 7]);
            lotofacilDto.ValorPagoQuinzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 8]);
        }

        private static void PreencherGanhadores(LotofacilDto lotofacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotofacilDto.QuantidadeGanhadoresOnzePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 9]);
            lotofacilDto.QuantidadeGanhadoresDozePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 10]);
            lotofacilDto.QuantidadeGanhadoresTrezePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 11]);
            lotofacilDto.QuantidadeGanhadoresQuatorzePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 12]);
            lotofacilDto.QuantidadeGanhadoresQuinzePontos = Convert.ToInt32(colunasCaixa[18]);
        }

        private static void PreencherResultados(LotofacilDto lotofacilDto, string[] colunasCaixa)
        {
            for (int i = 2; i < 17; i++)
            {
                lotofacilDto.Resultado.Add(Convert.ToInt32(colunasCaixa[i]));
            }
        }

        private static void PreencherCidades(LotofacilDto lotofacilDto)
        {
            //for (int i = 19; i < (colunas.Length - 12); i++)
            //{
            //    //processamento da cidade/uf
            //}
        }

        private static void PreencherStatus(LotofacilDto lotofacilDto)
        {
            lotofacilDto.StatusDoConcurso = lotofacilDto.QuantidadeGanhadoresQuinzePontos > 0 ? StatusDoConcurso.Pago : StatusDoConcurso.Acumulado;
        }
    }
}
