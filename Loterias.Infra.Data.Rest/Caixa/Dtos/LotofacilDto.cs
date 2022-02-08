using Loterias.Infra.Data.Rest.Caixa.Enums;

namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class LotofacilDto
    {
        public LotofacilDto()
        {
            Resultado = new List<int>();
        }

        public LotofacilDto(string concurso, DateTime dataSorteio, List<int> resultado, List<LotofacilMunicipioDto> municipio,
            int quantidadeGanhadoresQuinzePontos, decimal valorPagoQuinzePontos, int quantidadeGanhadoresQuatorzePontos,
            decimal valorPagoQuatorzePontos, int quantidadeGanhadoresTrezePontos, decimal valorPagoTrezePontos, int quantidadeGanhadoresOnzePontos,
            decimal valorPagoOnzePontos, decimal valorArrecadado, decimal valorAcumulado, decimal valorSorteado)
        {
            Concurso = concurso;
            DataSorteio = dataSorteio;
            Resultado = resultado;
            Municipio = municipio;
            QuantidadeGanhadoresQuinzePontos = quantidadeGanhadoresQuinzePontos;
            ValorPagoQuinzePontos = valorPagoQuinzePontos;
            QuantidadeGanhadoresQuatorzePontos = quantidadeGanhadoresQuatorzePontos;
            ValorPagoQuatorzePontos = valorPagoQuatorzePontos;
            QuantidadeGanhadoresTrezePontos = quantidadeGanhadoresTrezePontos;
            ValorPagoTrezePontos = valorPagoTrezePontos;
            QuantidadeGanhadoresOnzePontos = quantidadeGanhadoresOnzePontos;
            ValorPagoOnzePontos = valorPagoOnzePontos;
            ValorArrecadado = valorArrecadado;
            ValorAcumulado = valorAcumulado;
            ValorSorteado = valorSorteado;
        }

        public string Concurso { get; set; }
        public DateTime DataSorteio { get; set; }
        public StatusDoConcurso StatusDoConcurso { get; set; }
        public List<int> Resultado { get; set; }
        public List<LotofacilMunicipioDto> Municipio { get; set; }
        public int QuantidadeGanhadoresQuinzePontos { get; set; }
        public Decimal ValorPagoQuinzePontos { get; set; }
        public int QuantidadeGanhadoresQuatorzePontos { get; set; }
        public Decimal ValorPagoQuatorzePontos { get; set; }
        public int QuantidadeGanhadoresTrezePontos { get; set; }
        public Decimal ValorPagoTrezePontos { get; set; }
        public int QuantidadeGanhadoresOnzePontos { get; set; }
        public Decimal ValorPagoOnzePontos { get; set; }
        public int QuantidadeGanhadoresDozePontos { get; set; }
        public Decimal ValorPagoDozePontos { get; set; }
        public Decimal ValorArrecadado { get; set; }
        public Decimal ValorAcumulado { get; set; }
        public Decimal ValorSorteado { get; set; }
    }
}
