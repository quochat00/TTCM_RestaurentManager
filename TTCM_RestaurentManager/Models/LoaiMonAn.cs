using System;
using System.Collections.Generic;

namespace TTCM_RestaurentManager.Models;

public partial class LoaiMonAn
{
    public string MaLoaiMa { get; set; } = null!;

    public string TenLoaiMa { get; set; } = null!;

    public int? IsDeleted { get; set; }

    public virtual ICollection<MonAn> MonAns { get; set; } = new List<MonAn>();
}
