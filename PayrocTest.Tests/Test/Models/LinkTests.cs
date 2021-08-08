using System;
using System.Text;
using PayrocTest.Errors;
using PayrocTest.Models;
using PayrocTest.Tests.Helpers;
using Xunit;

namespace PayrocTest.Tests.Test.Models
{
    public class LinkTests
    {
        [Fact]
        public void GetShortenedUrl_LinkIdEqualsZero_ReturnsFailedResult()
        {
            var link = LinkFactory.Create();

            var result = link.GetShortenedUrl();

            result.AssertFailed(LinkErrors.IdNotSet);
        }

        [Fact]
        public void GetShortenedUrl_LinkIdIsNotEqualToZero_ReturnsSuccessResult()
        {
            var link = LinkFactory.CreateWithId(1);
            
            var result = link.GetShortenedUrl();

            result.AssertSucceeded();
        }

        [Fact]
        public void GetId_UrlPassedInIsNull_ReturnsFailureResult()
        {
            var result = Link.GetId(null);
            result.AssertFailed(GenericErrors.ParameterIsNull("shortUrl"));
        }

        [Fact]
        public void GetId_Base64EncodedStringIsNotValidLong_ReturnsFailureResult()
        {
            const string shortUrl = "abc";

            var bytes = Encoding.UTF8.GetBytes(shortUrl);
            var url = Convert.ToBase64String(bytes);

            var result = Link.GetId(url);

            result.AssertFailed(LinkErrors.ShortUrlCouldNotBeParsed(shortUrl));
        }

        [Fact]
        public void GetId_Base64EncodedStringIsValidLong_ReturnsSuccessResult()
        {
            var link = LinkFactory.CreateWithId(1);
            var url = link.GetShortenedUrl().Value;

            var result = Link.GetId(url);

            result.AssertSucceeded();
        }
    }
}
