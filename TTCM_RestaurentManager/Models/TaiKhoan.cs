using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class TaiKhoan
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Loai { get; set; }

    public string? MaNv { get; set; }

    public string? MaKh { get; set; }

    public int? IsDeleted { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
