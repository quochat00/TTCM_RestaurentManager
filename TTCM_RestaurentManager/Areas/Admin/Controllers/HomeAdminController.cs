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
                lstDish = ResDb.MonAns.AsNoTracking().Where(x => x.Gia > 15).ToList();
            }
            else if (price == "below")
            {
                lstDish = ResDb.MonAns.AsNoTracking().Where(x => x.Gia <= 15).ToList();
            }
            return View(lstDish);
        }
        [Route("addDish")]
        [HttpGet]
        public IActionResult AddDish()
        {
            ViewBag.MaLoaiMa = new SelectList(ResDb.LoaiMonAns.ToList(), "MaLoaiMa", "TenLoaiMa");
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("addDish")]
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
        [Route("deleteDish")]
        public IActionResult DeleteDish(string madish, string malma)
        {
            // Lấy danh sách tour cần xóa dựa trên mã tour
            var listdish = ResDb.MonAns.Where(x => x.MaMa == madish);

            // Kiểm tra xem mã nhân viên có tồn tại trong danh sách tour hay không
            var loai = ResDb.LoaiMonAns.Where(x => x.MaLoaiMa == malma).FirstOrDefault();
            //var cttour = Tourdb.Cttours.Where(x => x.MaCttour == mact).FirstOrDefault();
            if (loai != null)
            {
                foreach (var item in listdish)
                {
                    if (item.MaLoaiMa == malma)
                    {
                        return RedirectToAction("listDish");
                    }
                }
            }

            // Update trường IsDeleted của các món ăn trong danh sách và lưu thay đổi
            if (listdish != null)
            {
                foreach (var item in listdish)
                {
                    item.IsDeleted = 1;
                }
                ResDb.SaveChanges();
            }

            // Update trường IsDeleted của món ăn dựa trên mã món ăn và mã loại món ăn và lưu thay đổi
            var dish = ResDb.MonAns.FirstOrDefault(x => x.MaMa == madish && x.MaLoaiMa == malma);
            if (dish != null)
            {
                dish.IsDeleted = 1;
                ResDb.SaveChanges();
            }

            // Chuyển hướng đến trang danh sách món ăn
            return RedirectToAction("listDish");
        }

        /////BookTable
        [Route("listTable")]
        public IActionResult LishTable(string table)
        {
            var lstTable = ResDb.DatBans.ToList();
            if (table == "all")
            {
                lstTable = ResDb.DatBans.AsNoTracking().ToList();
            }
            if (table == "2")
            {
                lstTable = ResDb.DatBans.AsNoTracking().Where(x => x.SoLuongNguoi == 2).ToList();
            }
            else if (table == "3")
            {
                lstTable = ResDb.DatBans.AsNoTracking().Where(x => x.SoLuongNguoi == 3).ToList();
            }
            else if (table == "4")
            {
                lstTable = ResDb.DatBans.AsNoTracking().Where(x => x.SoLuongNguoi == 4).ToList();
            }
            else if (table == "5")
            {
                lstTable = ResDb.DatBans.AsNoTracking().Where(x => x.SoLuongNguoi == 5).ToList();
            }
            else if (table == "6")
            {
                lstTable = ResDb.DatBans.AsNoTracking().Where(x => x.SoLuongNguoi == 6).ToList();
            }
            return View(lstTable);
        }
        /////ListStaff
        [Route("listStaff")]
        public IActionResult ListStaff()
        {
            var lstStaff = ResDb.NhanViens.ToList();
            return View(lstStaff);
        }
    }
}
