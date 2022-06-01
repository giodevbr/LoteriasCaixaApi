using System.Text;

namespace Loterias.Util.Converters
{
    public static class Base64
    {
        public static string ToBase64(string text)
        {
            var bytes = Encoding.ASCII.GetBytes(text);
            var base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        public static string ToString(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var text = Encoding.UTF8.GetString(bytes);

            return text;
        }
    }
}
