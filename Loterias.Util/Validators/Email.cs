using System.Text.RegularExpressions;

namespace Loterias.Util.Validators
{
    public static class Email
    {
        public static bool Validar(string email)
        {
            try
            {
                var regex = ObterRegex();

                var match = regex.Match(email);

                return match.Success;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static Regex ObterRegex()
        {
            var regex = string.Format(@"{0}{1}",@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                                                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            return new Regex(regex);
        }
    }
}
