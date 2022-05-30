using Loterias.Core.Dtos;
using Loterias.Core.Interfaces;
using Loterias.Domain.Dtos;
using Loterias.Domain.Interfaces;
using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;
using Loterias.Util.Resources;
using System.Net.Mail;
using System.Text;

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
            ValidarCamposObrigatorios(usuarioDto);

            if (_notificacaoDeDominio.HasNotifications())
                return;

            ValidarEmail(usuarioDto.Email);
            ValidarSenha(usuarioDto.Senha);

            if (_notificacaoDeDominio.HasNotifications())
                return;

            await ValidarSeExisteOutroUsuarioComOMesmoEmail(usuarioDto.Email);

            if (_notificacaoDeDominio.HasNotifications())
                return;

            var novoUsuario = InstanciarUsuario(usuarioDto);

            await _usuarioRepository.CadastrarAsync(novoUsuario);
        }

        public async Task ValidarLogin(LoginDto loginDto)
        {
            ValidarCamposObrigatorios(loginDto);

            if (!_notificacaoDeDominio.HasNotifications())
            {
                var usuario = await _usuarioRepository.ObterPorEmail(loginDto.Email);

                if (usuario == null)
                    _notificacaoDeDominio.AddNotification(new Notification(StringResources.EmailOuSenhaInvalido));

                if (_notificacaoDeDominio.HasNotifications())
                    return;

                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (!ValidarSenhaLogin(usuario.Senha, loginDto.Senha))
                    _notificacaoDeDominio.AddNotification(new Notification(StringResources.EmailOuSenhaInvalido));
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }

        private async Task ValidarSeExisteOutroUsuarioComOMesmoEmail(string email)
        {
            if (await _usuarioRepository.ExisteOutroUsuarioComOMesmoEmail(email))
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.EmailVinculadoComOutroUsuario));
        }

        private void ValidarEmail(string email)
        {
            email = email.Trim();

            if (email.EndsWith("."))
            {
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.EmailInvalido));
                return;
            }

            try
            {
                var mailAddress = new MailAddress(email);

                if (mailAddress.Address != email)
                    _notificacaoDeDominio.AddNotification(new Notification(StringResources.EmailInvalido));
            }
            catch
            {
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.EmailInvalido));
            }
        }

        private void ValidarCamposObrigatorios(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.CamposNaoPreenchidos));
                return;
            }

            if (string.IsNullOrEmpty(usuarioDto.Nome))
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.NomeNaoInformado));

            if (string.IsNullOrEmpty(usuarioDto.Email))
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.EmailNaoInformado));

            if (string.IsNullOrEmpty(usuarioDto.Senha))
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.SenhaNaoInformada));
        }

        private void ValidarCamposObrigatorios(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.CamposNaoPreenchidos));
                return;
            }

            if (string.IsNullOrEmpty(loginDto.Email))
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.EmailNaoInformado));

            if (string.IsNullOrEmpty(loginDto.Senha))
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.SenhaNaoInformada));
        }

        private static Usuario InstanciarUsuario(UsuarioDto usuarioDto)
        {
            usuarioDto.Nome = usuarioDto.Nome.Trim();
            usuarioDto.Email = usuarioDto.Email.Trim();
            usuarioDto.Senha = usuarioDto.Senha.Trim();

            usuarioDto.Nome = usuarioDto.Nome.ToUpper();
            usuarioDto.Email = usuarioDto.Email.ToUpper();

            var senhaTratada = TratarSenha(usuarioDto.Senha);

            return new Usuario(usuarioDto.Nome, usuarioDto.Email, senhaTratada);
        }

        private void ValidarSenha(string senha)
        {
            if (senha.Length < 10)
                _notificacaoDeDominio.AddNotification(new Notification(StringResources.SenhaInvalida));
        }

        private static bool ValidarSenhaLogin(string senhaEncoded, string senhaInformada)
        {
            var senhaEncodedString = DestratarSenha(senhaEncoded);

            if (senhaEncodedString != senhaInformada)
                return false;

            return true;
        }

        private static string DestratarSenha(string senha)
        {
            var bytes = Convert.FromBase64String(senha);
            var text = Encoding.UTF8.GetString(bytes);

            return text;
        }

        private static string TratarSenha(string senha)
        {
            var bytes = Encoding.ASCII.GetBytes(senha);
            var base64 = Convert.ToBase64String(bytes);

            return base64;
        }
    }
}
