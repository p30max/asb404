using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asb404.Models
{
    public class Banner
    {
        [Key]
        public virtual int Id { get; set; }
        [StringLength(40, MinimumLength = 0, ErrorMessage = "Invalid")]
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        [Display(Name = "عنوان")]
        public virtual string Header { get; set; }
        [StringLength(80, MinimumLength = 0, ErrorMessage = "Invalid")]
        [Required(ErrorMessage = "موضوع را وارد کنید")]
        [Display(Name = "توضیحات")]
        public virtual string Ptext { get; set; }
        [StringLength(50,MinimumLength =0,ErrorMessage ="Invalid")]
        [Required(ErrorMessage ="ادرس سایت را وارد کنید")]
        [Display(Name = "لینک")]
        public virtual string Link { get; set; }
        [StringLength(150,MinimumLength =0,ErrorMessage ="Invalid")]
        //[Required(ErrorMessage ="عکس مورد نظر را وارد کنید")]
        [Display(Name ="عکس")]
        public virtual string Image { get; set; }

    }
}