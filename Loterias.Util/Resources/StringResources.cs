using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterias.Util.Resources
{
    public class StringResources
    {
        public static string FormatarResource(string principal, params object[] args)
        {
            return string.Format(principal, args);
        }

        public static string FormatarResourceToLower(string principal, params string[] args)
        {
            if (args == null)
                return principal;

            return FormatarResource(principal, args.Select(x => x.ToLower()).ToArray());
        }

        public const string Lotofacil = "lotofacil";

        public const string ErroGenerico = "Não foi possível concluir a exportação dos dados da API da Caixa.";
        public const string MunicipioNaoInformado = "NÃO INFORMADO";
        public const string UfNaoInformado = "NI";
        public const string CanalEletronico = "CANAL ELETRONICO";
        public const string NomeDeUfInvalido1 = "--";
        public const string NomeDeUfInvalido2 = "XX";

        public const string UrlMunicipioIbge = "localidades/estados/{0}/municipios";
        public const string UrlUfIbge = "localidades/estados";
        public const string FalhaNaComunicacaoComApi = "Não foi possível estabelecer uma comunicação com a API do IBGE.";
        public const string CodigoIbgeDaUfInvalido = "O Código IBGE informado é inválido.";

        public const string ExisteUfs = "Existem estados cadastrados, não é possível proceder com a carga.";
        public const string NaoExisteUfs = "Não existem estados cadastrados, não é possível proceder com a carga.";
        public const string ExisteMunicipios = "Existem munícipios cadastrados, não é possível proceder com a carga.";

        public const string CamposNaoPreenchidos = "Por favor, preencha os campos obrigatórios.";
        public const string NomeNaoInformado = "É obrigatório informar o nome.";
        public const string EmailNaoInformado = "É obrigatório informar o e-mail.";
        public const string SenhaNaoInformada = "É obrigatório informar a senha.";

        public const string SenhaInvalida = "A senha informada é inválida.";
        public const string EmailInvalido = "O e-mail informado é inválido.";
        public const string EmailNaoCadastrado = "O e-mail informado não está cadastrado.";
        public const string EmailVinculadoComOutroUsuario = "O e-mail informado está vinculado a outro usuário.";
        public const string EmailOuSenhaInvalido = "O e-mail ou a senha informada é inválido.";
    }
}
