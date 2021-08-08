namespace PayrocTest.Errors
{
    public class LinkErrors
    {
        public const string IdNotSet = "Cannot created shortened URL Before ID has been generated";

        public static string ShortUrlCouldNotBeParsed(string value)
        {
            return $"Given url could not be parsed into a valid ID: ({value})";
        }
    }
}
