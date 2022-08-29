using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asb404.Models
{
    public class Gallary
    {
        [Key]
        public virtual int id { get; set; }
        [Required(ErrorMessage ="پروژه را انتخاب کنید")]
        [Display(Name ="کد پروژه")]
        public virtual int idx { get; set; }
    
  
        [Display(Name ="نام عکس")]
        public virtual string image { get; set; }

        [Required(ErrorMessage = "توضیحات عکس را وارد کنید")]
        [Display(Name = "توضیحات")]
        public virtual string Discription { get; set; }
    
        [Display(Name = "تاریخ")]
        public virtual string Date { get; set; }
    }
}