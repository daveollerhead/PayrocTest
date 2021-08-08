using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PayrocTest.Controllers;
using PayrocTest.Services;
using PayrocTest.Tests.Helpers;
using PayrocTest.ViewModels;
using Xunit;

namespace PayrocTest.Tests.Test.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController _sut;

        private readonly Mock<IApplicationService> _mockApplicationService = new();

        public HomeControllerTests()
        {
            _sut = new HomeController(_mockApplicationService.Object);
        }

        [Fact]
        public async Task Index_NoUrlPassed_ReturnsView()
        {
            var result = await _sut.Index(null);

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);

            var model = viewResult.Model as LinkFormVm;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task Index_UrlPassedIsNotValidUrl_RedirectToLinkNotFound()
        {
            _mockApplicationService.Setup(x => x.GetLongUrl(It.IsAny<string>()))
                .ReturnsAsync(Result.Failure<string>("error"));

            var result = await _sut.Index("some invalid url");

            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);

            Assert.Equal(nameof(HomeController.LinkNotFound), redirectResult.ActionName);
        }

        [Fact]
        public async Task Index_UrlPassedIsValid_ReturnsRedirectToUrl()
        {
            _mockApplicationService.Setup(x => x.GetLongUrl(It.IsAny<string>()))
                .ReturnsAsync(Result.Success(LinkFactory.Create().Url.Value));

            var result = await _sut.Index("a valid url");

            var redirectResult = result as RedirectResult;
            Assert.NotNull(redirectResult);

            Assert.Equal(LinkFactory.GoogleDotCom, redirectResult.Url);
        }

        [Fact]
        public async Task NewLink_ModelStateIsInvalid_ReturnsIndex()
        {
            _sut.ModelState.AddModelError("Key", "Message");

            var result = await _sut.NewLink(new LinkFormVm());

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);

            Assert.Equal(nameof(HomeController.Index), viewResult.ViewName);
        }

        [Fact]
        public async Task NewLink_FailureCreatingShortLink_RedirectsToErrorAction()
        {
            _mockApplicationService.Setup(x => x.CreateShortUrl(It.IsAny<string>()))
                .ReturnsAsync(Result.Failure<string>("error"));

            var result = await _sut.NewLink(new LinkFormVm());

            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);

            Assert.Equal(nameof(HomeController.Error), redirectResult.ActionName);
        }

        [Fact]
        public async Task NewLink_SuccessfullyCreatesNewLink_ReturnsNewLinkView()
        {
            _mockApplicationService.Setup(x => x.CreateShortUrl(It.IsAny<string>()))
                .ReturnsAsync(Result.Success(LinkFactory.Create().Url.Value));

            _sut.Url = MockUrlHelperFactory.Create();

            var result = await _sut.NewLink(new LinkFormVm());

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);

            Assert.Equal(nameof(HomeController.NewLink), viewResult.ViewName);
        }
    }
}
