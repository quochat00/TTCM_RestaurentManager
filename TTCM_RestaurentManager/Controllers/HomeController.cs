using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO.Pipelines;
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
        [HttpGet]
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


    }
}