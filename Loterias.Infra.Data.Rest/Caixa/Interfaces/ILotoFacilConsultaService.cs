using Loterias.Infra.Data.Rest.Caixa.Dtos;

namespace Loterias.Infra.Data.Rest.Caixa.Interfaces
{
    public interface ILotoFacilConsultaService
    {
        public Task<List<LotofacilDto>> Consultar();
        public Task<LotofacilDto> ConsultarUltimoConcurso();
        public Task<LotofacilDto> ConsultarPorConcurso(string concurso);
        public Task<LotofacilDto> ConsultarPorDataDoSorteio(DateTime dataDoSorteio);
    }
}
