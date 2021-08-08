using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using PayrocTest.Services;
using PayrocTest.ViewModels;

namespace PayrocTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationService _applicationService;

        public HomeController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public async Task<IActionResult> Index(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return View(new LinkFormVm());

            var link = await _applicationService.GetLongUrl(url);
            if (link.IsFailure)
                return RedirectToAction(nameof(LinkNotFound));

            return Redirect(link.Value);
        }

        [HttpPost]
        public async Task<IActionResult> NewLink(LinkFormVm formVm)
        {
            if (!ModelState.IsValid)
                return View("Index", formVm);

            var shortUrl = await _applicationService.CreateShortUrl(formVm.Link);
            if (shortUrl.IsFailure)
                return RedirectToAction("Error");

            var fullShortUrl = Url.ActionLink("Index", "Home", new {url = shortUrl.Value});

            return View("NewLink", new NewLinkVm(fullShortUrl));
        }


        public IActionResult LinkNotFound()
        {
            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
