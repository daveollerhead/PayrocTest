namespace PayrocTest.Extensions
{
    public static class StringExtensions
    {
        public static string PadBase64(this string source)
        {
            for (var i = 0; i < source.Length % 4; i++)
                source += "=";

            return source;
        }
    }
}