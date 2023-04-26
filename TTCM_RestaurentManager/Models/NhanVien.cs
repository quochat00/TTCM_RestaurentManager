using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class NhanVien
{
    public int MaNv { get; set; }

    public string TenNv { get; set; } = null!;

    public string ChucVu { get; set; } = null!;

    public string? AnhNv { get; set; }

    public string Sdtnv { get; set; } = null!;

    public string EmailNv { get; set; } = null!;

    public int? IsDeleted { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
