using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
