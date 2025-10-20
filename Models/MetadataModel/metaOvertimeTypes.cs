using System;
using System.Collections.Generic;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaOvertimeTypes))]
    public partial class OvertimeTypes
    {

    }
    public class z_metaOvertimeTypes
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "唯一鍵值")]
        public string? BaseNo { get; set; }
        [Display(Name = "類別編號")]
        [Required(ErrorMessage = "{0}必須輸入!!")]
        public string? TypeNo { get; set; }
        [Display(Name = "類別名稱")]
        [Required(ErrorMessage = "{0}必須輸入!!")]
        public string? TypeName { get; set; }
        [Display(Name = "開始時間")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0}必須輸入!!")]
        public int StartHour { get; set; }
        [Display(Name = "結束時間")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0}必須輸入!!")]
        public int EndHour { get; set; }
        [Display(Name = "加班倍率")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0}必須輸入!!")]
        public decimal Rates { get; set; }
        [Display(Name = "備註")]
        public string? Remark { get; set; }

    }
}