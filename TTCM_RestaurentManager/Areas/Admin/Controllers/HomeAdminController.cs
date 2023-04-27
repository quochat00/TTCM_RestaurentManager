using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using TTCM_RestaurentManager.Models;
using System.Web;
using System.IO;
using X.PagedList;

namespace TTCM_RestaurentManager.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("homeadmin")]
    public class HomeAdminController : Controller
    {
        RestaurentManagerContext ResDb = new RestaurentManagerContext();
        //private readonly IWebHostEnvironment _env;

        //public HomeAdminController(IWebHostEnvironment env)
        //{
        //    _env = env;
        //}

        [Route("")]
		[Route("index")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("thongke")]
		[HttpPost]
		public IActionResult ThongKe(DateTime ngayTao)
		{
			// Lấy danh sách các hóa đơn được tạo từ ngày ngayTao trở đi
			List<HoaDon> hds = ResDb.HoaDons.Where(x => x.NgayTao >= ngayTao).ToList();

			// Lấy danh sách mã các món ăn từ các hóa đơn này
			List<int> dishIds = hds.Select(x => (int)x.MaMa).Distinct().ToList();

			// Lấy danh sách chi tiết các món ăn và tên các món ăn
			List<ThongKe> tks = new List<ThongKe>();
			var thongKes = (from hd in ResDb.HoaDons
							join ts in ResDb.MonAns on hd.MaMa equals ts.MaMa
							where dishIds.Contains((int)hd.MaMa)
							select new { hd.MaMa, hd.SoLuong, ts.TenMa, ts.AnhMa }).ToList();

			// Khởi tạo danh sách ThongKe, mỗi phần tử tương ứng với một món ăn
			foreach (int dishId in dishIds)
			{
				tks.Add(new ThongKe(dishId));
			}

			// Tính tổng số lượng của mỗi món ăn
			foreach (ThongKe tk in tks)
			{
				int dishId = tk.dishId;
				int count = 0;
				foreach (var thongKe in thongKes)
				{
					if (thongKe.MaMa == dishId)
					{
						count += (int)thongKe.SoLuong;
						tk.dishName = thongKe.TenMa;
                        tk.dishImg = thongKe.AnhMa;
					}
				}
				tk.soLuong = count;
			}

			// Sắp xếp danh sách theo số lượng giảm dần và trả về view
			List<ThongKe> tt = tks.OrderByDescending(x => x.soLuong).ToList();
			return View(tt);
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
        [Route("listbookdish")]
        public IActionResult KHTheoTour(int khdish)
        {
            var kh = (from c in ResDb.KhachHangs
                      join t in ResDb.HoaDons on c.MaKh equals t.MaKh
                      join q in ResDb.MonAns on t.MaMa equals q.MaMa
                      where q.MaMa == khdish
                      select c);
            return View(kh);
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
           
                dish.AnhMa = dish.formFile.FileName.Split(".")[0];
                ResDb.MonAns.Add(dish);
                ResDb.SaveChanges();
                return RedirectToAction("listDish");
            
            //if (uploadanh != null && uploadanh.Length > 0)
            //{
            //    int id = ResDb.MonAns.ToList().Last().MaMa;
            //    string fileName = "f" + id.ToString();
            //    //string extension = Path.GetExtension(uploadanh.FileName);
            //    string _FileName = fileName;// + extension;
            //    string _path = Path.Combine(_env.ContentRootPath, "Assets/images", _FileName);

            //    using (var stream = new FileStream(_path, FileMode.Create))
            //    {
            //        uploadanh.CopyTo(stream);
            //    }

            //    MonAn unv = ResDb.MonAns.FirstOrDefault(x => x.MaMa == id);
            //    unv.AnhMa = fileName;
            //    ResDb.SaveChanges();
            //}

            return View(dish);
        }


        [Route("editDish")]
        public IActionResult EditDish(int edish)
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
          
                
                dish.MaMa = eddish.MaMa;
                dish.TenMa = eddish.TenMa;
                dish.ChiTietMa = eddish.ChiTietMa;
                dish.Gia = eddish.Gia;
                dish.AnhMa = eddish.formFile.FileName.Split(".")[0];
                dish.MaLoaiMa = eddish.MaLoaiMa;
                dish.SoLuongMa = eddish.SoLuongMa;
                dish.Active = eddish.Active;
                dish.IsDeleted = eddish.IsDeleted;
                ResDb.SaveChanges();
                return RedirectToAction("listDish");
        
            return View(eddish);
        }
        [Route("deleteDish")]
        public IActionResult DeleteDish(int madish, int malma)
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


        [Route("editTable")]
        public IActionResult EditTable(int id)
		{
            var tableEdit = ResDb.DatBans.SingleOrDefault(x => x.Id == id);
			return View(tableEdit);
        }
		[Route("editTable")]
		[HttpPost]
		public IActionResult EditTable(DatBan datBan)
		{
			var table = ResDb.DatBans.Find(datBan.Id);
			if (ModelState.IsValid)
			{
				table.Id = datBan.Id;
				table.HoTen = datBan.HoTen;
				table.SoDienThoai = datBan.SoDienThoai;
				table.Email = datBan.Email;
				table.NgayDatBan = datBan.NgayDatBan;
				table.SoLuongNguoi = datBan.SoLuongNguoi;
				table.GhiChu = table.GhiChu;
				ResDb.SaveChanges();
				return RedirectToAction("listTable");
			}
			return View(table);
		}
        [Route("deleteTable")]
        public IActionResult DeleteTable(int maTable)
        {
            // Lấy danh sách tour cần xóa dựa trên mã tour
            var listtable = ResDb.DatBans.Where(x => x.Id == maTable);
            // Xóa tour dựa trên mã tour và lưu thay đổi
            var table = ResDb.DatBans.Find(maTable);
            if (table != null)
            {
                ResDb.Remove(table);
                ResDb.SaveChanges();
            }

            // Chuyển hướng đến trang lịch sử thêm tour
            return RedirectToAction("listTable");
        }


        /////ListStaff
        [Route("addStaff")]
        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [Route("addStaff")]
        [HttpPost]
        public IActionResult AddStaff(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                ResDb.NhanViens.Add(nv);
                ResDb.SaveChanges();
                return RedirectToAction("listStaff");
            }

            return View(nv);
        }

        [Route("listStaff")]
        public IActionResult ListStaff()
        {
            var lstStaff = ResDb.NhanViens.ToList();
            return View(lstStaff);
        }
        [Route("editStaff")]
        public IActionResult EditStaff(int manv)
        {
            var staffEdit = ResDb.NhanViens.SingleOrDefault(x => x.MaNv == manv);
            return View(staffEdit);
        }
        [Route("editStaff")]
        [HttpPost]
        public IActionResult EditStaff(NhanVien nv)
        {
            var staff = ResDb.NhanViens.Find(nv.MaNv);
            if (ModelState.IsValid)
            {
                staff.MaNv = nv.MaNv;
                staff.TenNv = nv.TenNv;
                staff.ChucVu = nv.ChucVu;
                staff.Sdtnv = nv.Sdtnv;
                staff.EmailNv = nv.EmailNv;
                staff.AnhNv = nv.AnhNv;
                ResDb.SaveChanges();
                return RedirectToAction("listStaff");
            }
            return View(staff);
        }
        [Route("deleteStaff")]
        public IActionResult DeleteStaff(int maStaff)
        {
            // Lấy danh sách tour cần xóa dựa trên mã tour
            var liststaff = ResDb.NhanViens.Where(x => x.MaNv == maStaff);
            // Xóa tour dựa trên mã tour và lưu thay đổi
            var staff = ResDb.NhanViens.Find(maStaff);
            if (staff != null)
            {
                ResDb.Remove(staff);
                ResDb.SaveChanges();
            }

            // Chuyển hướng đến trang lịch sử thêm tour
            return RedirectToAction("listStaff");
        }
    }
}
