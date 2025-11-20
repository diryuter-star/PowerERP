using System;
using System.Collections.Generic;

namespace powererp.Models;

public partial class Leaves
{
    public int Id { get; set; }

    public string? BaseNo { get; set; }

    public bool IsConfirm { get; set; }

    public bool IsCancel { get; set; }

    public string? SheetNo { get; set; }

    public DateTime? SheetDate { get; set; }

    public string? HandleNo { get; set; }

    public string? Remark { get; set; }
}
