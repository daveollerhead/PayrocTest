using System;
using System.Text;
using PayrocTest.Models;

namespace PayrocTest.Tests.Helpers
{
    internal static class LinkFactory
    {
        public const string GoogleDotCom = "https://google.com";

        public static Link Create()
        {
            return new(Url.Create(GoogleDotCom).Value);
        }

        public static Link CreateWithId(int id)
        {
            var link = new Link(Url.Create(GoogleDotCom).Value);
            link.SetProperty(x => x.Id, id);
            return link;
        }

        public static string GenerateEncodedUrlFragment(string url)
        {
            var bytes = Encoding.UTF8.GetBytes(url);
            return Convert.ToBase64String(bytes);
        }
    }
}
