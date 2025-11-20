using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaLeaves))]
    public partial class Leaves
    {
        [NotMapped]
        [Display(Name = "列號")]
        public int RowNo { get; set; }

        [NotMapped]
        [Display(Name = "承辦人員名稱")]
        public string? HandleName { get; set; }

        [NotMapped]
        [Display(Name = "表單狀態")]
        public string? StatusName
        {
            get
            {
                if (IsCancel) return "已作廢";
                else if (IsConfirm) return "已確認";
                else return "待確認";
            }
        }

        [NotMapped]
        [Display(Name = "狀態圖示")]
        public string? StatusIcon
        {
            get
            {
                if (IsCancel) return "fa fa-times-circle text-danger";
                else if (IsConfirm) return "fa-solid fa-circle-check text-success";
                else return "fa-solid fa-clock text-secondary";
            }
        }

    }
}

public class z_metaLeaves
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "唯一鍵值")]
    public string? BaseNo { get; set; }
    [Display(Name = "確認")]
    public bool IsConfirm { get; set; }
    [Display(Name = "作廢")]
    public bool IsCancel { get; set; }
    [Display(Name = "請假編號")]
    public string? SheetNo { get; set; }
    [Display(Name = "請假日期")]
    [Required(ErrorMessage = "請假日期為必填欄位")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime? SheetDate { get; set; }
    [Display(Name = "承辦人員")]
    public string? HandleNo { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}