namespace PayrocTest.ViewModels
{
    public class NewLinkVm
    {
        public string ShortenedUrl { get; }

        public NewLinkVm(string shortenedUrl)
        {
            ShortenedUrl = shortenedUrl;
        }
    }
}