using Loterias.Domain.Dtos;
using Loterias.Infra.Data.Model;

namespace Loterias.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<Usuario?> Login(LoginDto loginDto);

        string GerarToken(Usuario usuario);
    }
}
