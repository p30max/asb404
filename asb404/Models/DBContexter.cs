using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Asb404.Models
{
    public class DBContexter:DbContext
    {
        public virtual DbSet<Users> Users { set; get; }
        public virtual DbSet<Project> Project { set; get; }
        public virtual  DbSet<Gallary> Gallaries { get; set; }
        public virtual DbSet<Subscribe> Subscribes { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
    }
}