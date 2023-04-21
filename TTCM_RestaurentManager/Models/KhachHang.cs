using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class KhachHang
{
    public string MaKh { get; set; } = null!;

    public string AnhKh { get; set; } = null!;

    public string TenKh { get; set; } = null!;

    public string Sdtkh { get; set; } = null!;

    public string EmailKh { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public int? IsDeleted { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
