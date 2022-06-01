using Loterias.Infra.Data.Model;

namespace Loterias.Infra.Data.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario?> ObterPorEmail(string email);

        Task<bool> EmailEmUso(string email);
    }
}
