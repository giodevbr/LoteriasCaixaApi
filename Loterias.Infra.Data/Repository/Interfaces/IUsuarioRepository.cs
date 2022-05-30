using Loterias.Infra.Data.Model;

namespace Loterias.Infra.Data.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<bool> ExisteOutroUsuarioComOMesmoEmail(string email);

        Task<Usuario?> ObterPorEmail(string email);
    }
}
