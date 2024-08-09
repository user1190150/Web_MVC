using Microsoft.AspNetCore.Mvc;

namespace HarisWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
