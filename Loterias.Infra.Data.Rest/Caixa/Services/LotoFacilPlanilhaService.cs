using Loterias.Core.Enums;
using Loterias.Core.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Dtos;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Loterias.Util.Resources;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;

namespace Loterias.Infra.Data.Rest.Caixa.Services
{
    public class LotoFacilPlanilhaService : ILotoFacilPlanilhaService
    {
        private readonly IDomainNotification _notificacaoDeDominio;
        private readonly IConfiguration _configuration;
        private readonly string _urlPortalDeLoteriasLotofacil;

        public LotoFacilPlanilhaService(IDomainNotification notificacaoDeDominio, 
                                        IConfiguration configuration)
        {
            _notificacaoDeDominio = notificacaoDeDominio;
            _configuration = configuration;
            _urlPortalDeLoteriasLotofacil = _configuration.GetSection("Apis:UrlPortalDeLoteriasLotofacil").Value;
        }

        public async Task<List<LotoFacilPlanilhaDto>> ConsultarPlanilhaTodos()
        {
            var retorno = await ConsultaPortaDeLoteriasLotofacil();

            return retorno;
        }

        public async Task<LotoFacilPlanilhaDto> ConsultarPlanilhaPorConcurso(string concurso)
        {
            var retorno = await ConsultaPortaDeLoteriasLotofacil();
            var concursoRetorno = retorno.Where(c => c.Concurso == concurso).First();

            return concursoRetorno;
        }

        private async Task<List<LotoFacilPlanilhaDto>> ConsultaPortaDeLoteriasLotofacil()
        {
            var client = new RestClient();
            var request = new RestRequest(_urlPortalDeLoteriasLotofacil, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK ||
                response.Content == string.Empty ||
                response.Content == null)
                return new List<LotoFacilPlanilhaDto>();

            var retorno = TratarRetorno(response.Content);

            return retorno;
        }

        private List<LotoFacilPlanilhaDto> TratarRetorno(string retorno)
        {
            var listaLotoFacil = new List<LotoFacilPlanilhaDto>();

            try
            {
                var tabelaCaixa = TransformarTabelaHtmlEmArray(retorno);

                foreach (var linha in tabelaCaixa)
                {
                    var lotofacilDto = new LotoFacilPlanilhaDto();
                    
                    var colunasCaixa = TransformarLinhaHtmlEmArray(linha);
                    colunasCaixa = RemoverTagsHtmlDasColunas(colunasCaixa);
                    
                    var totalDeColunas = colunasCaixa.Length;

                    PreencherInformacoesDoSorteio(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherResultados(lotofacilDto, colunasCaixa);
                    PreencherGanhadores(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherValoresPagos(lotofacilDto, colunasCaixa, totalDeColunas);
                    PreencherStatus(lotofacilDto);

                    if (lotofacilDto.StatusConcurso == StatusConcurso.Pago)
                        PreencherCidades(lotofacilDto, colunasCaixa);

                    listaLotoFacil.Add(lotofacilDto);
                }
            }
            catch (Exception)
            {
                _notificacaoDeDominio.AddNotification(StringResources.ErroGenerico);
            }

            return listaLotoFacil;
        }

        private static string[] TransformarTabelaHtmlEmArray(string tabelaCaixa)
        {
            var tabelaCaixaComCabecalho = tabelaCaixa.Split("</thead>");

            var tabelaCaixaSemCabecalho = tabelaCaixaComCabecalho[1].ToLower()
                                                                    .Replace("\\r\\n", "")
                                                                    .Replace("<tbody><tr><td>1</td>", "<tr><td>1</td>")
                                                                    .Split("</tr></tbody><tbody>");

            return tabelaCaixaSemCabecalho;
        }

        private static string[] TransformarLinhaHtmlEmArray(string linhaTabelaCaixa)
        {
            return linhaTabelaCaixa.Split("</td><td>");
        }

        private static string[] RemoverTagsHtmlDasColunas(string[] colunasCaixa)
        {
            for (int i = 0; i < colunasCaixa.Length; i++)
            {
                colunasCaixa[i] = colunasCaixa[i].Replace("r$", "");
                colunasCaixa[i] = colunasCaixa[i].Replace("<td>", "");
                colunasCaixa[i] = colunasCaixa[i].Replace("<tr>", "");
                colunasCaixa[i] = colunasCaixa[i].Replace("<table>", "");
                colunasCaixa[i] = colunasCaixa[i].Replace("<tbody>", "");
                colunasCaixa[i] = colunasCaixa[i].Replace("</td>", "");
                colunasCaixa[i] = colunasCaixa[i].Replace("</tr>", "");
                colunasCaixa[i] = colunasCaixa[i].Replace("</table>", "");
                colunasCaixa[i] = colunasCaixa[i].Replace("</tbody>", "");
            }

            colunasCaixa = colunasCaixa.Where(x => x != "").ToArray();

            return colunasCaixa;
        } 

        private static void PreencherInformacoesDoSorteio(LotoFacilPlanilhaDto lotoFacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotoFacilDto.Concurso = colunasCaixa[0];
            lotoFacilDto.DataSorteio = Convert.ToDateTime(colunasCaixa[1]);
            lotoFacilDto.ValorArrecadado = Convert.ToDecimal(colunasCaixa[17]);
            lotoFacilDto.ValorSorteado = Convert.ToDecimal(colunasCaixa[totalDeColunas - 2]);
            lotoFacilDto.ValorAcumulado = Convert.ToDecimal(colunasCaixa[totalDeColunas - 3]);
        }

        private static void PreencherValoresPagos(LotoFacilPlanilhaDto lotoFacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotoFacilDto.ValorPagoOnzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 4]);
            lotoFacilDto.ValorPagoDozePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 5]);
            lotoFacilDto.ValorPagoTrezePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 6]);
            lotoFacilDto.ValorPagoQuatorzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 7]);
            lotoFacilDto.ValorPagoQuinzePontos = Convert.ToDecimal(colunasCaixa[totalDeColunas - 8]);
        }

        private static void PreencherGanhadores(LotoFacilPlanilhaDto lotoFacilDto, string[] colunasCaixa, int totalDeColunas)
        {
            lotoFacilDto.QuantidadeGanhadoresOnzePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 9]);
            lotoFacilDto.QuantidadeGanhadoresDozePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 10]);
            lotoFacilDto.QuantidadeGanhadoresTrezePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 11]);
            lotoFacilDto.QuantidadeGanhadoresQuatorzePontos = Convert.ToInt32(colunasCaixa[totalDeColunas - 12]);
            lotoFacilDto.QuantidadeGanhadoresQuinzePontos = Convert.ToInt32(colunasCaixa[18]);
        }

        private static void PreencherResultados(LotoFacilPlanilhaDto lotoFacilDto, string[] colunasCaixa)
        {
            for (int i = 2; i < 17; i++)
            {
                lotoFacilDto.Resultado.Add(Convert.ToInt32(colunasCaixa[i]));
            }
        }

        private static void PreencherCidades(LotoFacilPlanilhaDto lotoFacilDto, string[] colunasCaixa)
        {
            var ufs = new List<string>();
            var municipios = new List<string>();

            for (int i = 19; i < (colunasCaixa.Length - 12); i++)
            {
                var municipioOuUf = colunasCaixa[i].Trim().ToUpper();

                if (!string.IsNullOrEmpty(municipioOuUf))
                {
                    if (municipioOuUf.Length == 2 && municipioOuUf != StringResources.NomeDeUfInvalido1 && municipioOuUf != StringResources.NomeDeUfInvalido2)
                    {
                        ufs.Add(municipioOuUf);

                        if (ufs.Count > municipios.Count && municipios.Count > 0)
                            municipios.Add(StringResources.MunicipioNaoInformado);
                    }
                    else
                    if (municipioOuUf.Length > 0 && municipioOuUf != StringResources.CanalEletronico)
                    {
                        municipios.Add(municipioOuUf);
                    }
                }
            }

            if (ufs.Count == 0)
                lotoFacilDto.Municipio.Add(new LotoFacilMunicipioDto(StringResources.MunicipioNaoInformado, StringResources.UfNaoInformado));

            if (ufs.Count == 1)
            {
                if (municipios.Count == 1)
                    lotoFacilDto.Municipio.Add(new LotoFacilMunicipioDto(municipios.First(), ufs.First()));

                lotoFacilDto.Municipio.Add(new LotoFacilMunicipioDto(StringResources.MunicipioNaoInformado, ufs.First()));
            }
            else
            {
                if (municipios.Count == 0)
                    foreach (var uf in ufs)
                        lotoFacilDto.Municipio.Add(new LotoFacilMunicipioDto(StringResources.MunicipioNaoInformado, uf));
                else
                    for (int i = 0; i < ufs.Count; i++)
                        lotoFacilDto.Municipio.Add(new LotoFacilMunicipioDto(ufs[i], municipios[i]));
            }
        }

        private static void PreencherStatus(LotoFacilPlanilhaDto lotoFacilDto)
        {
            lotoFacilDto.StatusConcurso = lotoFacilDto.QuantidadeGanhadoresQuinzePontos > 0 ? StatusConcurso.Pago : StatusConcurso.Acumulado;
        }
    }
}
