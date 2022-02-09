using Loterias.Infra.Data.Rest.Caixa.Dtos;

namespace Loterias.Infra.Data.Rest.Caixa.Interfaces
{
    public interface ILotoFacilConsultaService
    {
        public Task<List<LotoFacilDto>> Consultar();
        public Task<LotoFacilDto> ConsultarUltimoConcurso();
        public Task<LotoFacilDto> ConsultarPorConcurso(string concurso);
        public Task<LotoFacilDto> ConsultarPorDataDoSorteio(DateTime dataDoSorteio);
    }
}
