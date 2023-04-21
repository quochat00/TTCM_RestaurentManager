using Microsoft.AspNetCore.Mvc;
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
                            return RedirectToAction("Index", "HomeCustomer");
                        default:
                            return View();
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(user);
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
