using Newtonsoft.Json;

namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class UltimosResultadosDuplaSenaDto
    {
        [JsonProperty("dezenasSegundoSorteio")]
        public List<string> DezenasSegundoSorteio { get; set; }

        [JsonProperty("quantidadeGanhadoresSegundoSorteio")]
        public int QuantidadeGanhadoresSegundoSorteio { get; set; }

        [JsonProperty("totalGanhadores")]
        public int TotalGanhadores { get; set; }

        [JsonProperty("acumulado")]
        public bool Acumulado { get; set; }

        [JsonProperty("concursoEspecial")]
        public bool ConcursoEspecial { get; set; }

        [JsonProperty("dataApuracao")]
        public string DataApuracao { get; set; }

        [JsonProperty("dataPorExtenso")]
        public string DataPorExtenso { get; set; }

        [JsonProperty("dezenas")]
        public List<string> Dezenas { get; set; }

        [JsonProperty("numeroDoConcurso")]
        public int NumeroDoConcurso { get; set; }

        [JsonProperty("quantidadeGanhadores")]
        public int QuantidadeGanhadores { get; set; }

        [JsonProperty("tipoJogo")]
        public string TipoJogo { get; set; }

        [JsonProperty("valorEstimadoProximoConcurso")]
        public double ValorEstimadoProximoConcurso { get; set; }

        [JsonProperty("valorPremio")]
        public double ValorPremio { get; set; }
    }
}
