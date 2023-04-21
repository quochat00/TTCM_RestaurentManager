using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Index(string MaLoaiMa)
        {
            var model = from ma in resDb.MonAns
                        where (string.IsNullOrEmpty(MaLoaiMa) || ma.MaLoaiMa == MaLoaiMa)
                        select ma;
            if (MaLoaiMa == "all")
            {
                model = resDb.MonAns;
            }

            ViewBag.MaLoaiMa = new SelectList(resDb.LoaiMonAns, "MaLoaiMa", "TenLoaiMa");
            return View(model.ToList());
        }
        [Route("booking")]
        [HttpGet]
        public IActionResult Booking(string dishId)
        {
            var dishDetail = (from t in resDb.MonAns
                              where t.MaMa == dishId
                              select new
                              {
                                  t.MaMa,
                                  t.TenMa,
                                  t.Gia,
                                  t.ChiTietMa,
                                  t.AnhMa,
                                  t.SoLuongMa
                              }).ToList();
            ViewBag.dishDetail = dishDetail;
            ViewBag.khachHang = getCurrentUser();
            return View();
        }
        [Route("booking")]
        [HttpPost]
        public IActionResult Booking(int soLuong, string dishId)
        {
            if (soLuong <= 0)
            {
                return BadRequest();
            }
            var dish = resDb.MonAns.SingleOrDefault(t => t.MaMa == dishId);
            if (dish == null)
            {
                return BadRequest();
            }
            var kh = getCurrentUser();
            var hd = new HoaDon
            {
                MaKh = kh.MaKh,
                MaMa = dish.MaMa,
                TongTien = dish.Gia * soLuong
            };
            resDb.HoaDons.Add(hd);
            resDb.SaveChanges();
            dish.SoLuongMa -= soLuong;
            resDb.SaveChanges();
            ViewBag.tenKhach = kh.TenKh;
            ViewBag.sdt = kh.Sdtkh;
            ViewBag.tenDish = dish.TenMa;
            ViewBag.soLuong = soLuong;
            ViewBag.tongTien = hd.TongTien;
            ViewBag.ngayDat = hd.NgayTao;
            return View("BookSuccessFully");
        }

        private KhachHang getCurrentUser()
        {
            var userName = HttpContext.User.Identity.Name;
            var khachhang = resDb.KhachHangs.FirstOrDefault(
                kh => kh.TaiKhoans.Any(tk => tk.Username == userName));
            return khachhang;
        }
        [Route("menu")]
        public IActionResult Menus(string maLoaiMa)
        {
            // Lấy danh sách món ăn từ database
            List<MonAn> monAnList = resDb.MonAns.ToList();
            // Nếu MaLoaiMa không có giá trị, lấy tất cả các món ăn
            if (string.IsNullOrEmpty(maLoaiMa))
            {
                ViewBag.MonAnList = resDb.MonAns.ToList();
            }
            // Lọc danh sách món ăn theo mã loại món ăn nếu có
            if (!string.IsNullOrEmpty(maLoaiMa))
            {
                monAnList = monAnList.Where(m => m.MaLoaiMa == maLoaiMa).ToList();
            }

            // Đưa danh sách món ăn và SelectList của các loại món ăn vào ViewBag để truyền sang View
            ViewBag.MonAnList = monAnList;
            ViewBag.MaLoaiMa = new SelectList(resDb.LoaiMonAns.ToList(), "MaLoaiMa", "TenLoaiMa", "");


            return View(monAnList);
        }

        [Route("bookOK")]
        [HttpGet]
        public IActionResult BookOK()
        {
            return View();
        }


        [Route("booktable")]
        [HttpGet]
        public IActionResult BookTables()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [Route("booktable")]
        [HttpPost]
        public IActionResult BookTables(DatBan ban)
        {
            if (ModelState.IsValid)
            {
                resDb.DatBans.Add(ban);
                resDb.SaveChanges();
                return RedirectToAction("bookOK");
            }
            return View(ban);
        }
        [Route("about")]
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

    }
}
