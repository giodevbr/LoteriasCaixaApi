using Newtonsoft.Json;

namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class ListaMunicipioUfGanhadoresDto
    {
        [JsonProperty("ganhadores")]
        public int Ganhadores { get; set; }

        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        [JsonProperty("nomeFatansiaUL")]
        public string NomeFatansiaUl { get; set; }

        [JsonProperty("posicao")]
        public int Posicao { get; set; }

        [JsonProperty("serie")]
        public string Serie { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }
    }
}
