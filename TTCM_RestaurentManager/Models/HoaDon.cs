using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int? MaMa { get; set; }

    public int? MaKh { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? SoLuong { get; set; }

    public decimal? TongTien { get; set; }

    public int? IsDeleted { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual MonAn? MaMaNavigation { get; set; }
}
