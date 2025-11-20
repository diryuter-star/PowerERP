using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaLeavesDetail))]
    public partial class LeavesDetail
    {
        [NotMapped]
        [Display(Name = "請假類別")]
        public string? TypeName { get; set; }
        [NotMapped]
        [Display(Name = "員工姓名")]
        public string? EmpName { get; set; }
    }
}
public class z_metaLeavesDetail
{

    [Key]
    public int Id { get; set; }
    [Display(Name = "父階編號")]
    public string? ParentNo { get; set; }
    [Display(Name = "員工編號")]
    public string? EmpNo { get; set; }
    [Display(Name = "請假類別")]
    public string? TypeNo { get; set; }
    [Display(Name = "開始時間")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime? StartTime { get; set; }
    [Display(Name = "結束時間")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime? EndTime { get; set; }
    [Display(Name = "請假時數")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Hours { get; set; }
    [Display(Name = "請假原因")]
    public string? ReasonText { get; set; }
    [Display(Name = "轉特休假")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int SpecialDays { get; set; }
    [Display(Name = "可特休天數")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int TotSpecialDays { get; set; }
    [Display(Name = "累計特休天數")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int SumSpecialDays { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}