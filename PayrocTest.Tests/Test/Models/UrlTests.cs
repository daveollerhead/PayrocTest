using PayrocTest.Errors;
using PayrocTest.Tests.Helpers;
using PayrocTest.Models;
using Xunit;

namespace PayrocTest.Tests.Test.Models
{
    public class UrlTests
    {
        private const string GoogleDotCom = "https://google.com";

        [Fact]
        public void Create_UrlIsNull_ReturnsFailedResult()
        {
            var result = Url.Create(null);
            result.AssertFailed(UrlErrors.NotValidUrl);
        }

        [Fact]
        public void Create_UrlIsNotWellFormed_ReturnsFailedResult()
        {
            var result = Url.Create("random string");
            result.AssertFailed(UrlErrors.NotValidUrl);
        }

        [Fact]
        public void Create_UrlIsWellFormed_ReturnsSuccessResult()
        {
            var result = Url.Create(GoogleDotCom);
            result.AssertSucceeded();
        }
    }
}
