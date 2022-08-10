using Newtonsoft.Json;

namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class LotofacilDto
    {
        [JsonProperty("acumulado")]
        public bool? Acumulado { get; set; }

        [JsonProperty("dataApuracao")]
        public DateTime DataApuracao { get; set; }

        [JsonProperty("dataProximoConcurso")]
        public DateTime DataProximoConcurso { get; set; }

        [JsonProperty("dezenasSorteadasOrdemSorteio")]
        public List<string> DezenasSorteadasOrdemSorteio { get; set; }

        [JsonProperty("exibirDetalhamentoPorCidade")]
        public bool? ExibirDetalhamentoPorCidade { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("indicadorConcursoEspecial")]
        public int? IndicadorConcursoEspecial { get; set; }

        [JsonProperty("listaDezenas")]
        public List<string> ListaDezenas { get; set; }

        [JsonProperty("listaDezenasSegundoSorteio")]
        public object? ListaDezenasSegundoSorteio { get; set; }

        [JsonProperty("listaMunicipioUFGanhadores")]
        public List<ListaMunicipioUfGanhadoresDto> ListaMunicipioUFGanhadores { get; set; }

        [JsonProperty("listaRateioPremio")]
        public List<ListaRateioPremioDto> ListaRateioPremio { get; set; }

        [JsonProperty("listaResultadoEquipeEsportiva")]
        public object? ListaResultadoEquipeEsportiva { get; set; }

        [JsonProperty("localSorteio")]
        public string? LocalSorteio { get; set; }

        [JsonProperty("nomeMunicipioUFSorteio")]
        public string? NomeMunicipioUFSorteio { get; set; }

        [JsonProperty("nomeTimeCoracaoMesSorte")]
        public string? NomeTimeCoracaoMesSorte { get; set; }

        [JsonProperty("numero")]
        public int? Numero { get; set; }

        [JsonProperty("numeroConcursoAnterior")]
        public int? NumeroConcursoAnterior { get; set; }

        [JsonProperty("numeroConcursoFinal_0_5")]
        public int? NumeroConcursoFinal05 { get; set; }

        [JsonProperty("numeroConcursoProximo")]
        public int? NumeroConcursoProximo { get; set; }

        [JsonProperty("numeroJogo")]
        public int? NumeroJogo { get; set; }

        [JsonProperty("observacao")]
        public string? Observacao { get; set; }

        [JsonProperty("premiacaoContingencia")]
        public object? PremiacaoContingencia { get; set; }

        [JsonProperty("tipoJogo")]
        public string? TipoJogo { get; set; }

        [JsonProperty("tipoPublicacao")]
        public int? TipoPublicacao { get; set; }

        [JsonProperty("ultimoConcurso")]
        public bool? UltimoConcurso { get; set; }

        [JsonProperty("valorArrecadado")]
        public double? ValorArrecadado { get; set; }

        [JsonProperty("valorAcumuladoConcurso_0_5")]
        public double? ValorAcumuladoConcurso05 { get; set; }

        [JsonProperty("valorAcumuladoConcursoEspecial")]
        public double? ValorAcumuladoConcursoEspecial { get; set; }

        [JsonProperty("valorAcumuladoProximoConcurso")]
        public double? ValorAcumuladoProximoConcurso { get; set; }

        [JsonProperty("valorEstimadoProximoConcurso")]
        public double? ValorEstimadoProximoConcurso { get; set; }

        [JsonProperty("valorSaldoReservaGarantidora")]
        public double? ValorSaldoReservaGarantidora { get; set; }

        [JsonProperty("valorTotalPremioFaixaUm")]
        public double? ValorTotalPremioFaixaUm { get; set; }
    }
}
