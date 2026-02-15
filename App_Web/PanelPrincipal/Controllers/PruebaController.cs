using Microsoft.AspNetCore.Mvc;

namespace PanelPrincipal.Controllers
{
    public class PruebaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
