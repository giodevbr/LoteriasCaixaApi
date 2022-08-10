using Loterias.Infra.Data.Rest.Caixa.Dtos;

namespace Loterias.Infra.Data.Rest.Caixa.Interfaces
{
    public interface ILotoFacilCaixaApiService
    {
        Task<LotofacilDto> ObterResultadoPorConcurso(string concurso);
    }
}
