using Loterias.Core.Interfaces;
using Loterias.Domain.Dtos;
using Loterias.Domain.Interfaces;
using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;
using Loterias.Util.Converters;
using Loterias.Util.Resources;
using Loterias.Util.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Loterias.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IDomainNotification _notificacaoDeDominio;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IDomainNotification domainNotification,
                            IUsuarioRepository usuarioRepository,
                            IConfiguration configuration)
        {
            _notificacaoDeDominio = domainNotification;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<Usuario?> Login(LoginDto loginDto)
        {
            ValidarLogin(loginDto);

            if (!_notificacaoDeDominio.HasNotifications())
            {
                var usuario = await _usuarioRepository.ObterPorEmail(loginDto.Email.Trim());

                if (usuario == null)
                    _notificacaoDeDominio.AddNotification(StringResources.EmailNaoCadastrado);
                else
                {
                    if (!ValidarSenha(usuario.Senha, loginDto.Senha))
                        _notificacaoDeDominio.AddNotification(StringResources.EmailOuSenhaInvalido);

                    if (!_notificacaoDeDominio.HasNotifications())
                        return usuario;
                }
            }

            return null;
        }

        public string GerarToken(Usuario usuario)
        {
            var issuer = _configuration["Jwt:Issuer"];

            var audience = _configuration["Jwt:Audience"];

            var claims = new[]
            {
                new Claim("Id", usuario.Id.ToString()),
                new Claim("Name", usuario.Nome),
                new Claim("Email", usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.Now.AddMinutes(30);

            JwtSecurityToken token = new
            (
               issuer: issuer,
               audience: audience,
               claims: claims,
               expires: expiration,
               signingCredentials: credentials
            );

            var objectToken = new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

            var json = JsonConvert.SerializeObject(objectToken);

            return json;
        }

        private void ValidarLogin(LoginDto loginDto)
        {
            ValidarCamposObrigatorios(loginDto);

            if (_notificacaoDeDominio.HasNotifications())
                return;

            if (!Email.Validar(loginDto.Email))
                _notificacaoDeDominio.AddNotification(StringResources.EmailInvalido);
        }

        private void ValidarCamposObrigatorios(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                _notificacaoDeDominio.AddNotification(StringResources.CamposNaoPreenchidos);
                return;
            }

            if (string.IsNullOrEmpty(loginDto.Email))
                _notificacaoDeDominio.AddNotification(StringResources.EmailNaoInformado);

            if (string.IsNullOrEmpty(loginDto.Senha))
                _notificacaoDeDominio.AddNotification(StringResources.SenhaNaoInformada);
        }

        private static bool ValidarSenha(string senhaCorretaBase64, string senhaInformada)
        {
            var senhaCorretaString = Base64.ToString(senhaCorretaBase64);

            if (senhaCorretaString != senhaInformada)
                return false;

            return true;
        }
    }
}
