using Microsoft.AspNetCore.Mvc;


namespace Stocks.API.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return new RedirectResult("~/swagger/index.html");
        }
    }
}
