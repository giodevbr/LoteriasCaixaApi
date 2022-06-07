using Loterias.Infra.Data.Rest.Caixa.Dtos;

namespace Loterias.Infra.Data.Rest.Caixa.Interfaces
{
    public interface IUltimosResultadosService
    {
        Task<UltimosResultadosDto> ConsultarUltimosResultados();
    }
}
