using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTCM_RestaurentManager.Models;

namespace TTCM_RestaurentManager.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("homeadmin")]
    public class HomeAdminController : Controller
    {
        RestaurentManagerContext ResDb = new RestaurentManagerContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("listDish")]
        public IActionResult LishDish(string price)
        {
            var lstDish = ResDb.MonAns.ToList();
            if (price == "all")
            {
                lstDish = ResDb.MonAns.AsNoTracking().ToList();
            }
            if (price == "above")
            {
                lstDish = ResDb.MonAns.AsNoTracking().Where(x => x.Gia > 5000000).ToList();
            }
            else if (price == "below")
            {
                lstDish = ResDb.MonAns.AsNoTracking().Where(x => x.Gia <= 5000000).ToList();
            }
            return View(lstDish);
        }
        [Route("adddish")]
        [HttpGet]
        public IActionResult AddDish()
        {
            ViewBag.MaLoaiMa = new SelectList(ResDb.LoaiMonAns.ToList(), "MaLoaiMa", "TenLoaiMa");
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("addtour")]
        [HttpPost]
        public IActionResult AddDish(MonAn dish)
        {
            if (ModelState.IsValid)
            {
                ResDb.MonAns.Add(dish);
                ResDb.SaveChanges();
                return RedirectToAction("listDish");
            }
            return View(dish);
        }
    }
}
