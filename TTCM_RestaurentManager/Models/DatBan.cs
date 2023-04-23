using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class DatBan
{
    public int Id { get; set; }

    public string HoTen { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTimeOffset NgayDatBan { get; set; }

    public int SoLuongNguoi { get; set; }

    public string? GhiChu { get; set; }
}
