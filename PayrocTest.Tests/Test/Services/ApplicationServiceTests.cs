using System;
using System.Text;
using System.Threading.Tasks;
using Moq;
using PayrocTest.Data;
using PayrocTest.Data.Repositories;
using PayrocTest.Errors;
using PayrocTest.Models;
using PayrocTest.Services;
using PayrocTest.Tests.Helpers;
using Xunit;

namespace PayrocTest.Tests.Test.Services
{
    public class ApplicationServiceTests
    {
        private readonly ApplicationService _sut;

        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new ();
        private readonly Mock<IRepository<Link, long>> _mockRepository = new();

        public ApplicationServiceTests()
        {
            _sut = new ApplicationService(_mockUnitOfWork.Object);
        }
        
        [Fact]
        public async Task GetLongUrl_ShortUrlIsNull_ReturnsFailedResult()
        {
            var result = await _sut.GetLongUrl(null);
            result.AssertFailed(GenericErrors.ParameterIsNull("shortUrl"));
        }

        [Fact]
        public async Task GetLongUrl_Base64EncodedStringIsNotValidLong_ReturnsFailedResult()
        {
            const string shortUrl = "abc";
            var url = LinkFactory.GenerateEncodedUrlFragment(shortUrl);

            var result = await _sut.GetLongUrl(url);
            result.AssertFailed(LinkErrors.ShortUrlCouldNotBeParsed(shortUrl));
        }

        [Fact]
        public async Task GetLongUrl_NoLinkExistsInDatabaseWithGivenKey_ReturnsFailedResult()
        {
            const string shortUrl = "1";
            var url = LinkFactory.GenerateEncodedUrlFragment(shortUrl);

            _mockRepository.Setup(x => x.Find(It.IsAny<long>()))
                .ReturnsAsync(default(Link));

            _mockUnitOfWork.Setup(x => x.GetRepository<Link, long>())
                .Returns(_mockRepository.Object);

            var result = await _sut.GetLongUrl(url);
            result.AssertFailed(GenericErrors.EntityNotFound(nameof(Link)));
        }

        [Fact]
        public async Task GetLongUrl_LinkExistsInDatabaseWithGivenKey_ReturnsSuccessResult()
        {
            const string shortUrl = "1";
            var url = LinkFactory.GenerateEncodedUrlFragment(shortUrl);

            _mockRepository.Setup(x => x.Find(It.IsAny<long>()))
                .ReturnsAsync(LinkFactory.Create);

            _mockUnitOfWork.Setup(x => x.GetRepository<Link, long>())
                .Returns(_mockRepository.Object);

            var result = await _sut.GetLongUrl(url);
            result.AssertSucceeded();
        }


    }
}
