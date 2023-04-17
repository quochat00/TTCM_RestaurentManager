using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TTCM_RestaurentManager.Models;
using X.PagedList;

namespace TTCM_RestaurentManager.Controllers
{
    [Route("home")]
    [Route("")]
    public class HomeController : Controller
    {
        RestaurentManagerContext resDb = new RestaurentManagerContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("index")]
        public IActionResult Index()
        {
            var listMA = resDb.MonAns;
            
            List<MonAn> lst = new List<MonAn>(listMA);
            ViewBag.MaLoaiMa = new SelectList(resDb.LoaiMonAns, "MaLoaiMa", "TenLoaiMa");
            return View(lst);
        }

        [Route("menu")]
        public IActionResult Menus()
        {
            var listDish = resDb.MonAns;
            var listSanPham = resDb.MonAns.AsNoTracking().OrderBy(x => x.TenMa);
            List<MonAn> lst = new List<MonAn>(listSanPham);
            return View(lst);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}