using Loterias.Infra.Data.Rest.Ibge.Dtos;

namespace Loterias.Infra.Data.Rest.Ibge.Interfaces
{
    public interface IIbgeConsultaService
    {
        public Task<List<UfDto>> ConsultarUfs();
        public Task<List<MunicipioDto>> ConsultarMunicipiosPorUf(int codigoIbgeUf);
    }
}
