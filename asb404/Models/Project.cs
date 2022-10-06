using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asb404.Models
{
    public class Project
    {
        [Key]
        public virtual int Id { set; get; }
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Invalid")]
        [Required(ErrorMessage = "نام مشتری را وارد کنید")]
        [Display(Name = "نام مشتری")]
        public virtual string Customer { get; set; }
        [Required(ErrorMessage ="گروه را انتخاب کنید")]
        [Display(Name = "گروه مشتری")]
        public virtual int GroupId { set; get; } = 0;
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Invalid")]
        [Required(ErrorMessage = "موضوع پروژه را مشخص کنید")]
        [Display(Name = "موضوع پروژه")]
        public virtual  string Subject { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid")]
        [Required(ErrorMessage = "*")]
        [Display(Name = "نوع قرارداد")]
        public virtual string NoProject { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid")]
        [Required(ErrorMessage = "*")]
        [Display(Name = "وضعیت")]
        public virtual string noprivate { get; set; }
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Invalid")]
      
        [Display(Name = "سایت")]
        public virtual string Website { get; set; }
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Invalid")]
        [Required(ErrorMessage = "آدرس را وارد کنید")]
        [Display(Name = "آدرس")]
        public virtual string Address { get; set; }
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Invalid")]
      
        [Display(Name = "توضیحات")]
        public virtual string Discription { get; set; }
        [Display(Name = "انتخاب عکس")]
        public virtual string image { get; set; }

    }
}