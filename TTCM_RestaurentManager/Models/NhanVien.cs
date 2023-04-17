using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public string ChucVu { get; set; } = null!;

    public string AnhNv { get; set; } = null!;

    public string Sdtnv { get; set; } = null!;

    public string EmailNv { get; set; } = null!;

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
