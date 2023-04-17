using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class MonAn
{
    public string MaMa { get; set; } = null!;

    public string TenMa { get; set; } = null!;

    public string ChiTietMa { get; set; } = null!;

    public decimal Gia { get; set; }

    public string AnhMa { get; set; } = null!;

    public string? MaLoaiMa { get; set; }

    public int SoLuongMa { get; set; }

    public int? Active { get; set; }

    public int? IsDeleted { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual LoaiMonAn? MaLoaiMaNavigation { get; set; }
}
