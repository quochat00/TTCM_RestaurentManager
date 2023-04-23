using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class LoaiMonAn
{
    public int MaLoaiMa { get; set; }

    public string TenLoaiMa { get; set; } = null!;

    public virtual ICollection<MonAn> MonAns { get; set; } = new List<MonAn>();
}
