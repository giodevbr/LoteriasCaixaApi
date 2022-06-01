using Loterias.Domain.Dtos;

namespace Loterias.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task Cadastrar(UsuarioDto usuarioDto);
    }
}
