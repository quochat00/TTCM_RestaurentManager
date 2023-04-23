using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
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
        public IActionResult Index(int MaLoaiMa)
        {
            var model = from ma in resDb.MonAns
                        where (MaLoaiMa == 0 || ma.MaLoaiMa == MaLoaiMa)
                        select ma;
            if (MaLoaiMa == 0)
            {
                model = resDb.MonAns;
            }
            var kh = getCurrentUser();
            ViewBag.tenKhach = kh.TenKh;
            ViewBag.MaLoaiMa = new SelectList(resDb.LoaiMonAns, "MaLoaiMa", "TenLoaiMa");
            return View(model.ToList());
        }

        [Route("dishDetail")]
		public IActionResult DishDetail(int madish)
		{
			var dishDetail = resDb.MonAns
				.Where(t => t.MaMa == madish)
				.FirstOrDefault();

			ViewBag.DishDetail = dishDetail;
			return View();
		}


		[Route("booking")]
        [HttpGet]
        public IActionResult Booking(int dishId)
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
        public IActionResult Booking(int soLuong, int dishId)
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
                SoLuong = soLuong,
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
        [Route("history")]
        public IActionResult History(int MaKH = 0)
        {
            var kh = getCurrentUser();
            ViewBag.tenKhach = kh.TenKh;
            if (MaKH == 0)
            {
                MaKH = getMaKH();
            }
            var history = (from hd in resDb.HoaDons
                           join ts in resDb.MonAns on hd.MaMa equals ts.MaMa
                           join Kh in resDb.KhachHangs on hd.MaKh equals Kh.MaKh
                           where String.Equals(Kh.MaKh, MaKH)
                           select new { MonAn = ts, HoaDon = hd, KhachHang = Kh }).OrderByDescending(x => x.HoaDon.NgayTao).ToList();
            ViewBag.History = history;
            return View();
        }





        [Route("myAccount")]
        public IActionResult MyAccount()
        {
            var kh = getCurrentUser();
            var userName = HttpContext.Session.GetString("UserName");
            var tk = (from t in resDb.TaiKhoans where userName.Equals(t.Username) select t).ToList();
            if (tk == null)
            {
                RedirectToAction("Access", "Login");
            }
            ViewBag.tenKhach = kh.TenKh;
            ViewBag.diaChi = kh.DiaChi;
            ViewBag.sdt = kh.Sdtkh;
            ViewBag.userName = tk[0].Username;
            ViewBag.email = tk[0].Email;
            return View();
        }

        private int getMaKH()
        {
            int MaKHang = 0;
            var userName = HttpContext.Session.GetString("UserName");
            var khachhang = (from kh in resDb.KhachHangs
                             join tk in resDb.TaiKhoans on kh.MaKh equals tk.MaKh
                             where String.Equals(tk.Username, userName)
                             select kh).ToList();
            if (khachhang.Count > 0)
            {
                MaKHang = khachhang[0].MaKh;
            }
            return MaKHang;
        }
        private KhachHang getCurrentUser()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var khachhang = (from kh in resDb.KhachHangs
                             join tk in resDb.TaiKhoans on kh.MaKh equals tk.MaKh
                             where String.Equals(tk.Username, userName)
                             select kh).ToList();
            return khachhang[0];
        }
        [Route("menu")]
        public IActionResult Menus(int maLoaiMa)
        {
            // Lấy danh sách món ăn từ database
            List<MonAn> monAnList = resDb.MonAns.ToList();

            // Nếu MaLoaiMa không có giá trị, lấy tất cả các món ăn
            if (maLoaiMa == 0)
            {
                ViewBag.MonAnList = resDb.MonAns.ToList();
            }

            // Lọc danh sách món ăn theo mã loại món ăn nếu có
            if (maLoaiMa != 0)
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
