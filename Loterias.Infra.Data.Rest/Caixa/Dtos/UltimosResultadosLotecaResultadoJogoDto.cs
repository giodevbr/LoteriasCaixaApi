using Newtonsoft.Json;

namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class UltimosResultadosLotecaResultadoJogoDto
    {
        [JsonProperty("sequencia")]
        public int Sequencia { get; set; }

        [JsonProperty("equipeUm")]
        public string EquipeUm { get; set; }

        [JsonProperty("equipeDois")]
        public string EquipeDois { get; set; }

        [JsonProperty("siglaUFEquipeUm")]
        public string SiglaUFEquipeUm { get; set; }

        [JsonProperty("siglaUFEquipeDois")]
        public string SiglaUFEquipeDois { get; set; }

        [JsonProperty("golsEquipeUm")]
        public int GolsEquipeUm { get; set; }

        [JsonProperty("golsEquipeDois")]
        public int GolsEquipeDois { get; set; }

        [JsonProperty("diaSemana")]
        public string DiaSemana { get; set; }

        [JsonProperty("icSorteioResultado")]
        public int IcSorteioResultado { get; set; }

        [JsonProperty("siglaPaisUm")]
        public string SiglaPaisUm { get; set; }

        [JsonProperty("siglaPaisDois")]
        public string SiglaPaisDois { get; set; }
    }
}