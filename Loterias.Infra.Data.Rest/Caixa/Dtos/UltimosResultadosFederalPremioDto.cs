using Newtonsoft.Json;

namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class UltimosResultadosFederalPremioDto
    {
        [JsonProperty("bilhete")]
        public string Bilhete { get; set; }

        [JsonProperty("posicao")]
        public int Posicao { get; set; }

        [JsonProperty("valorPremio")]
        public double ValorPremio { get; set; }
    }
}
