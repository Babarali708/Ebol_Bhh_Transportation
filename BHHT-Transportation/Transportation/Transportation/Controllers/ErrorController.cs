using Microsoft.AspNetCore.Mvc;

namespace Transportation.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
