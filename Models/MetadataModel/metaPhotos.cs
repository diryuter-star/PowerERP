using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaPhotos))]
    public partial class Photos
    {
        [NotMapped]
        [Display(Name = "圖片")]
        public string? ImageUrl
        {
            get
            {
                if (FolderName != null && FolderName.Length > 0)
                {
                    string str_filename = $"wwwroot\\images\\portfolios\\{FolderName}.jpg";
                    string str_filepath = Path.Combine(Directory.GetCurrentDirectory(), str_filename);
                    //檔案存在
                    if (File.Exists(str_filepath)) return $"~/images/portfolios/{FolderName}.jpg";
                }
                return "~/images/nopic.jpg";
            }
        }

        [NotMapped]
        [Display(Name = "詳細說明(部份內容)")]
        public string? ShortDetailText
        {
            get
            {
                if (DetailText != null && DetailText.Length > 20)
                {
                    return DetailText.Substring(0, 20) + "...";
                }
                else
                {
                    return DetailText;
                }
            }
        }
    }
}

public class z_metaPhotos
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "分類代號")]
    public string? CodeNo { get; set; }
    [Display(Name = "檔案名稱")]
    public string? FolderName { get; set; }
    [Display(Name = "圖片名稱")]
    public string? PhotoName { get; set; }
    [Display(Name = "銷售價格")]
    public string? PriceName { get; set; }
    [Display(Name = "銷售日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateOnly SaleDate { get; set; }
    [Display(Name = "詳細說明")]
    public string? DetailText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}