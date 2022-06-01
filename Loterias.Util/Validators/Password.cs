using System.Text.RegularExpressions;

namespace Loterias.Util.Validators
{
    public static class Password
    {
        private static readonly int _tamanhoMinimo = 10;
        private static readonly int _tamanhoMaximo = 50;

        private static readonly bool _complexidadeAlta = true;
        private static readonly bool _caracterEspecial = true;

        public static bool Validar(string senha)
        {
            try
            {
                var regex = ObterRegex();

                var match = regex.Match(senha);

                return match.Success;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static Regex ObterRegex()
        {
            var regex = "^.*(?=.{" + _tamanhoMinimo.ToString() + "," + _tamanhoMaximo.ToString() + "})";

            if (_complexidadeAlta)
            {
                regex += "(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])";
                
                if (_caracterEspecial)
                {
                    regex += "(?=.*[@#$%^&+=])";
                }
            }
            else
            {
                regex += "(?=.*[a-zA-Z0-9@#$%^&+=])";
            }

            regex += ".*$";

            return new Regex(regex);
        }
    }
}
