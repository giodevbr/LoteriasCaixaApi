using RestSharp;
using System.Net;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Dtos;
using Loterias.Infra.Data.Rest.Caixa.Enums;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Loterias.Core.Dtos;
using Loterias.Util.Resources;

namespace Loterias.Infra.Data.Rest.Caixa.Services
{
    public class LotoFacilConsultaService : ILotoFacilConsultaService
    {
        private readonly IDomainNotification _notificacaoDeDominio;

        public LotoFacilConsultaService(IDomainNotification notificacaoDeDominio)
        {
            _notificacaoDeDominio = notificacaoDeDominio;
        }

        public async Task<List<LotoFacilDto>> Consultar()
        {
            return await ConsultarCaixa();
        }

        public async Task<LotoFacilDto> ConsultarUltimoConcurso()
        {
            var retorno = await ConsultarCaixa();
            var concursoRetorno = retorno.LastOrDefault();

            return concursoRetorno;
        }

        public async Task<LotoFacilDto> ConsultarPorConcurso(string concurso)
        {
            var retorno = await ConsultarCaixa();
            var concursoRetorno = retorno.Where(c => c.Concurso == concurso).FirstOrDefault();

            return concursoRetorno;
        }

        public async Task<LotoFacilDto> ConsultarPorDataDoSorteio(DateTime dataDoSorteio)
        {
            var retorno = await ConsultarCaixa();
            var concursoRetorno = retorno.Where(c => c.DataSorteio == dataDoSorteio).FirstOrDefault();

            return concursoRetorno;
        }

        private async Task<List<LotoFacilDto>> ConsultarCaixa()
        {
            var client = new RestClient(CaixaResources.UrlBase);
            var request = new RestRequest(CaixaResources.UrlLotofacil, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK || 
                response.Content == string.Empty || 
                response.Content == null)
                return new List<LotoFacilDto>();

            var retorno = TratarRetorno(response.Content);

            return retorno;
        }

        private List<LotoFacilDto> TratarRetorno(string retorno)
        {
            var listaLotoFacil = new List<LotoFacilDto>();

            try
            {
                var tabelaCaixa = TransformarTabelaHtmlEmArray(retorno);

                foreach (var linha in tabelaCaixa)
                {
                    var lotofacilDto = new LotoFacilDto();
                    var arrayDeColunas = TransformarLinhaHtmlEmArray(linha);
                    var colunasCaixa = RemoverLinhasComLixoDoArrayDeColunas(arrayDeColunas);
                    var totalDeColunas = colunasCaixa.Length;

                    PreencherInformacoesDoSorteio(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherValoresPagos(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherGanhadores(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherResultados(lotofacilDto, colunasCaixa);
                    PreencherCidades(lotofacilDto, colunasCaixa);
                    PreencherStatus(lotofacilDto);

                    listaLotoFacil.Add(lotofacilDto);
                }
            }
            catch (Exception)
            {
                _notificacaoDeDominio.AddNotification(new Notification(ComumResources.ErroGenerico));
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

        private static void PreencherInformacoesDoSorteio(LotoFacilDto lotoFacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotoFacilDto.Concurso = colunasCaixa[0];
            lotoFacilDto.DataSorteio = Convert.ToDateTime(colunasCaixa[1]);
            lotoFacilDto.ValorArrecadado = Convert.ToDecimal(colunasCaixa[17]);
            lotoFacilDto.ValorSorteado = Convert.ToDecimal(colunasCaixa[totalDeColunas - 2]);
            lotoFacilDto.ValorAcumulado = Convert.ToDecimal(colunasCaixa[totalDeColunas - 3]);
        }

        private static void PreencherValoresPagos(LotoFacilDto lotoFacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotoFacilDto.ValorPagoOnzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 4]);
            lotoFacilDto.ValorPagoDozePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 5]);
            lotoFacilDto.ValorPagoTrezePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 6]);
            lotoFacilDto.ValorPagoQuatorzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 7]);
            lotoFacilDto.ValorPagoQuinzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 8]);
        }

        private static void PreencherGanhadores(LotoFacilDto lotoFacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotoFacilDto.QuantidadeGanhadoresOnzePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 9]);
            lotoFacilDto.QuantidadeGanhadoresDozePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 10]);
            lotoFacilDto.QuantidadeGanhadoresTrezePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 11]);
            lotoFacilDto.QuantidadeGanhadoresQuatorzePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 12]);
            lotoFacilDto.QuantidadeGanhadoresQuinzePontos = Convert.ToInt32(colunasCaixa[18]);
        }

        private static void PreencherResultados(LotoFacilDto lotoFacilDto, string[] colunasCaixa)
        {
            for (int i = 2; i < 17; i++)
            {
                lotoFacilDto.Resultado.Add(Convert.ToInt32(colunasCaixa[i]));
            }
        }

        private static void PreencherCidades(LotoFacilDto lotoFacilDto, string[] colunasCaixa)
        {
            //for (int i = 19; i < (colunas.Length - 12); i++)
            //{
            //    //processamento da cidade/uf
            //}
        }

        private static void PreencherStatus(LotoFacilDto lotoFacilDto)
        {
            lotoFacilDto.StatusDoConcurso = lotoFacilDto.QuantidadeGanhadoresQuinzePontos > 0 ? StatusDoConcurso.Pago : StatusDoConcurso.Acumulado;
        }
    }
}
