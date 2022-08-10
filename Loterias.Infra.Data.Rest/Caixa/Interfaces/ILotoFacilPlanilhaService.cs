using Loterias.Infra.Data.Rest.Caixa.Dtos;

namespace Loterias.Infra.Data.Rest.Caixa.Interfaces
{
    public interface ILotoFacilPlanilhaService
    {
        public Task<List<LotoFacilPlanilhaDto>> ConsultarPlanilhaTodos();
        public Task<LotoFacilPlanilhaDto> ConsultarPlanilhaPorConcurso(string concurso);
    }
}
