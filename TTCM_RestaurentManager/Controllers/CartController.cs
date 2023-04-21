using Microsoft.AspNetCore.Mvc;

namespace TTCM_RestaurentManager.Controllers
{
    public class CartController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }
    }
}
