using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asb404.Models
{

    public class Users
    {
        [Key]
        public virtual int Id { set; get; }
        public string NameFamily { get; set; }
        public string UserName { get; set; }
  
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobail { get; set; }
        public string Role { get; set; }
        public bool isApproved { get; set; }
        public int Ostan { get; set; }
        public string Shahrestan { get; set; }
        public string Address{ get; set; }
        public string PostCode { get; set; }

    }


}