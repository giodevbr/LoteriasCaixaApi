using Newtonsoft.Json;

namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class ListaRateioPremioDto
    {
        [JsonProperty("descricaoFaixa")]
        public string DescricaoFaixa { get; set; }

        [JsonProperty("faixa")]
        public int Faixa { get; set; }

        [JsonProperty("numeroDeGanhadores")]
        public int NumeroDeGanhadores { get; set; }

        [JsonProperty("valorPremio")]
        public double ValorPremio { get; set; }
    }
}
