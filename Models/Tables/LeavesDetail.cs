using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class LeavesDetail
{


    public int Id { get; set; }

    public string? ParentNo { get; set; }

    public string? EmpNo { get; set; }

    public string? TypeNo { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int Hours { get; set; }

    public string? ReasonText { get; set; }

    public int SpecialDays { get; set; }

    public int TotSpecialDays { get; set; }

    public int SumSpecialDays { get; set; }

    public string? Remark { get; set; }
}
