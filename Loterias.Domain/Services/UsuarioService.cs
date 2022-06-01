using Loterias.Core.Interfaces;
using Loterias.Domain.Dtos;
using Loterias.Domain.Interfaces;
using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;
using Loterias.Util.Converters;
using Loterias.Util.Resources;
using Loterias.Util.Validators;

namespace Loterias.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDomainNotification _notificacaoDeDominio;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IDomainNotification domainNotification,
                              IUsuarioRepository usuarioRepository)
        {
            _notificacaoDeDominio = domainNotification;
            _usuarioRepository = usuarioRepository;
        }

        public async Task Cadastrar(UsuarioDto usuarioDto)
        {
            await ValidarCadastro(usuarioDto);

            if (_notificacaoDeDominio.HasNotifications())
                return;

            var novoUsuario = InstanciarUsuario(usuarioDto);

            await _usuarioRepository.AddAsync(novoUsuario);
        }

        private async Task ValidarCadastro(UsuarioDto usuarioDto)
        {
            ValidarCamposObrigatorios(usuarioDto);

            if (_notificacaoDeDominio.HasNotifications())
                return;

            if (!Email.Validar(usuarioDto.Email))
                _notificacaoDeDominio.AddNotification(StringResources.EmailInvalido);

            if (!Password.Validar(usuarioDto.Senha))
                _notificacaoDeDominio.AddNotification(StringResources.SenhaInvalida);

            if (_notificacaoDeDominio.HasNotifications())
                return;

            if (await _usuarioRepository.EmailEmUso(usuarioDto.Email.Trim()))
                _notificacaoDeDominio.AddNotification(StringResources.EmailVinculadoComOutroUsuario);
        }

        private void ValidarCamposObrigatorios(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                _notificacaoDeDominio.AddNotification(StringResources.CamposNaoPreenchidos);
                return;
            }

            if (string.IsNullOrEmpty(usuarioDto.Nome))
                _notificacaoDeDominio.AddNotification(StringResources.NomeNaoInformado);

            if (string.IsNullOrEmpty(usuarioDto.Email))
                _notificacaoDeDominio.AddNotification(StringResources.EmailNaoInformado);

            if (string.IsNullOrEmpty(usuarioDto.Senha))
                _notificacaoDeDominio.AddNotification(StringResources.SenhaNaoInformada);
        }

        private Usuario InstanciarUsuario(UsuarioDto usuarioDto)
        {
            usuarioDto.Nome = usuarioDto.Nome.Trim();
            usuarioDto.Email = usuarioDto.Email.Trim();
            usuarioDto.Senha = usuarioDto.Senha.Trim();

            usuarioDto.Nome = usuarioDto.Nome.ToUpper();
            usuarioDto.Email = usuarioDto.Email.ToUpper();

            var senhaTratada = Base64.ToBase64(usuarioDto.Senha);

            return new Usuario(usuarioDto.Nome, usuarioDto.Email, senhaTratada);
        }
    }
}
