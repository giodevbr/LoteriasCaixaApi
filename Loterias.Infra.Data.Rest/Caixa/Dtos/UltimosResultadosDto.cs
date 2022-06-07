using Newtonsoft.Json;

namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class UltimosResultadosDto
    {
        [JsonProperty("diaDeSorte")]
        public UltimosResultadosDiaDeSorteDto DiaDeSorte { get; set; }

        [JsonProperty("duplasena")]
        public UltimosResultadosDuplaSenaDto Duplasena { get; set; }

        [JsonProperty("federal")]
        public UltimosResultadosFederalDto Federal { get; set; }

        [JsonProperty("loteca")]
        public UltimosResultadosLotecaDto Loteca { get; set; }

        [JsonProperty("lotofacil")]
        public UltimosResultadosLotoFacilDto Lotofacil { get; set; }

        [JsonProperty("lotomania")]
        public UltimosResultadosLotoManiaDto Lotomania { get; set; }

        [JsonProperty("maisMilionaria")]
        public UltimosResultadosMaisMilionariaDto MaisMilionaria { get; set; }

        [JsonProperty("megasena")]
        public UltimosResultadosMegaSenaDto Megasena { get; set; }

        [JsonProperty("quina")]
        public UltimosResultadosQuinaDto Quina { get; set; }

        [JsonProperty("superSete")]
        public UltimosResultadosSuperSeteDto SuperSete { get; set; }

        [JsonProperty("timemania")]
        public UltimosResultadosTimemaniaDto Timemania { get; set; }
    }
}
