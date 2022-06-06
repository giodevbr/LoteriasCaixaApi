using Loterias.Infra.Data.Rest.Caixa.Dtos;

namespace Loterias.Infra.Data.Rest.Caixa.Interfaces
{
    public interface ILotoFacilService
    {
        public Task<List<LotoFacilDto>> ConsultarPlanilhaTodos();
        public Task<LotoFacilDto> ConsultarPlanilhaPorConcurso(string concurso);
    }
}
