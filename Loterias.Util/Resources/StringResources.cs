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

        public const string UrlBaseCaixa = "http://loterias.caixa.gov.br/";
        public const string UrlLotofacil = "wps/portal/loterias/landing/lotofacil/!ut/p/a1/04_Sj9CPykssy0xPLMnMz0vMAfGjzOLNDH0MPAzcDbz8vTxNDRy9"
                                         + "_Y2NQ13CDA0sTIEKIoEKnN0dPUzMfQwMDEwsjAw8XZw8XMwtfQ0MPM2I02-AAzgaENIfrh-FqsQ9wBmoxN_FydLAGAgNTKEK8DkR"
                                         + "rACPGwpyQyMMMj0VAcySpRM!/dl5/d5/L2dBISEvZ0FBIS9nQSEh/pw/Z7_HGK818G0K85260Q5OIRSC42046/res/id=historico"
                                         + "HTML/c=cacheLevelPage/=/";

        public const string ErroGenerico = "Não foi possível concluir a exportação dos dados da API da Caixa.";
        public const string MunicipioNaoInformado = "NÃO INFORMADO";
        public const string UfNaoInformado = "NI";
        public const string CanalEletronico = "CANAL ELETRONICO";
        public const string NomeDeUfInvalido1 = "--";
        public const string NomeDeUfInvalido2 = "XX";

        public const string UrlBaseIbge = "https://servicodados.ibge.gov.br/api/v1/";
        public const string UrlMunicipioIbge = "localidades/estados/{0}/municipios";
        public const string UrlUfIbge = "localidades/estados";
        public const string FalhaNaComunicacaoComApi = "Não foi possível estabelecer uma comunicação com a API do IBGE.";
        public const string CodigoIbgeDaUfInvalido = "O Código IBGE informado é inválido.";
    }
}
