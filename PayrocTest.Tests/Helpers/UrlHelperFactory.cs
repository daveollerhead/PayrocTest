using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace PayrocTest.Tests.Helpers
{
    internal static class MockUrlHelperFactory
    {
        public static IUrlHelper Create()
        {
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.ActionContext)
                .Returns(new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()));

            return mockUrlHelper.Object;
        }
    }
}
