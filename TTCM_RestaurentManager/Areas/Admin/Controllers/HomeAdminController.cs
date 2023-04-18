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

        [Route("editDish")]
        public IActionResult EditDish(string edish)
        {
            var dishedit = ResDb.MonAns.SingleOrDefault(x => x.MaMa == edish);
            ViewBag.MaLoaiMa = new SelectList(ResDb.LoaiMonAns.ToList(), "MaLoaiMa", "TenLoaiMa");
            return View(dishedit);
        }

        [Route("editDish")]
        [HttpPost]
        public IActionResult EditDish(MonAn eddish)
        {
            var dish = ResDb.MonAns.Find(eddish.MaMa);
            if (ModelState.IsValid)
            {
                dish.MaMa = eddish.MaMa;
                dish.TenMa = eddish.TenMa;
                dish.ChiTietMa = eddish.ChiTietMa;
                dish.Gia = eddish.Gia;
                dish.AnhMa = eddish.AnhMa;
                dish.MaLoaiMa = eddish.MaLoaiMa;
                dish.SoLuongMa = eddish.SoLuongMa;
                dish.Active = eddish.Active;
                dish.IsDeleted = eddish.IsDeleted;
                ResDb.SaveChanges();
                return RedirectToAction("listDish");
            }
            return View(eddish);
        }

    }
}
