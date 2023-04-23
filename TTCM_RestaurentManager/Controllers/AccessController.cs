using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using TTCM_RestaurentManager.Models;

namespace TTCM_RestaurentManager.Controllers
{
    public class AccessController : Controller
    {
        RestaurentManagerContext db = new RestaurentManagerContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Login(TaiKhoan user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var obj = db.TaiKhoans.Where(x => x.Username == user.Username && x.Pass == user.Pass).FirstOrDefault();
                if (obj != null)
                {
                    HttpContext.Session.SetString("UserName", obj.Username.ToString());
                    switch (obj.Loai)
                    {
                        case "admin":
                            return RedirectToAction("Index", "HomeAdmin");
                        case "customer":
                            // Kiểm tra xem thông tin khách hàng đã có chưa
                            var customer = db.KhachHangs.FirstOrDefault(kh => kh.MaKh == obj.MaKh);
                            if (customer != null)
                            {
                                return RedirectToAction("Index", "HomeCustomer");
                            }
                            else
                            {
                                return RedirectToAction("AddCustomer");
                            }
                        default:
                            return View();
                    }
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(user);
        }
        [Route("addCustomer")]
        public IActionResult AddCustomer()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [Route("addCustomer")]
        [HttpPost]
        public IActionResult AddCustomer(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();

                // Lấy MaKH vừa được thêm vào
                int maKH = kh.MaKh;

                // Lấy thông tin tài khoản đăng nhập hiện tại
                string username = HttpContext.Session.GetString("UserName");
                var taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.Username == username);

                // Thêm MaKH vào tài khoản đăng nhập
                taiKhoan.MaKh = maKH;
                db.TaiKhoans.Update(taiKhoan);
                db.SaveChanges();

                return RedirectToAction("Index", "HomeCustomer");
            }
            return View(kh);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(TaiKhoan user)
        {
            //if (ModelState.IsValid)
            //{

            //}
            // Kiểm tra xem tài khoản đã tồn tại chưa
            var u = db.TaiKhoans.Where(x => x.Username.Equals(user.Username)).FirstOrDefault();
            if (u != null)
            {
                ModelState.AddModelError("TaiKhoan", "Tài khoản đã được sử dụng");
                return RedirectToAction("Login", "Access");
            }

            // Thêm người dùng mới vào cơ sở dữ liệu
            db.TaiKhoans.Add(user);
            db.SaveChanges();

            // Đăng nhập người dùng mới đăng ký
            //HttpContext.Session.SetString("TaiKhoan", user.Taikhoan1);
            return RedirectToAction("Login", "Access");
        }
    }
}
