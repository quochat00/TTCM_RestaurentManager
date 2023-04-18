using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTCM_RestaurentManager.Models;

namespace TTCM_RestaurentManager.Controllers
{
    [Route("customer")]
    public class HomeCustomerController : Controller
    {
        RestaurentManagerContext resDb = new RestaurentManagerContext();
        private readonly ILogger<HomeCustomerController> _logger;

        public HomeCustomerController(ILogger<HomeCustomerController> logger)
        {
            _logger = logger;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {

            var listMA = resDb.MonAns;
            var listLMA = resDb.MonAns.AsNoTracking().OrderBy(x => x.TenMa);
            List<MonAn> lst = new List<MonAn>(listLMA);
            return View(lst);
        }
    }
}
