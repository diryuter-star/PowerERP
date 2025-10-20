using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    [ModelMetadataType(typeof(z_metaTestimonials))]
    public partial class Testimonials
    {
        [NotMapped]
        [Display(Name = "大頭照")]
        public string? ImageUrl
        {
            get
            {
                if (UserNo != null && UserNo.Length > 0)
                {
                    string str_filename = $"wwwroot\\images\\testimonials\\{UserNo}.jpg";
                    string str_filepath = Path.Combine(Directory.GetCurrentDirectory(), str_filename);
                    //檔案不存在
                    if (File.Exists(str_filepath)) return $"~/images/testimonials/{UserNo}.jpg";
                }
                return "~/images/testimonials/user.jpg";
            }
        }

        [NotMapped]
        [Display(Name = "訊息內容(部份內容)")]
        public string? ShortMessageText
        {
            get
            {
                if (MessageText != null && MessageText.Length > 20)
                {
                    return MessageText.Substring(0, 20) + "...";
                }
                else
                {
                    return MessageText;
                }
            }
        }
    }
}

public class z_metaTestimonials
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "提交日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime? SendDate { get; set; }
    [Display(Name = "使用者編號")]
    public string? UserNo { get; set; }
    [Display(Name = "使用者姓名")]
    public string? UserName { get; set; }
    [Display(Name = "職稱")]
    public string? TitleName { get; set; }
    [Display(Name = "星數")]
    public int StarCount { get; set; }
    [Display(Name = "訊息內容")]
    public string? MessageText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
