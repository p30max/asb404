using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asb404.Models
{
    public class Subscribe
    {
        [Key]
        public virtual int ID { set; get; }
        
        public virtual string Email { set; get; }
    }
}