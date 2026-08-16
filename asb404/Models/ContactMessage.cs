using System;
using System.ComponentModel.DataAnnotations;

namespace Asb404.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        [StringLength(4000)]
        public string Memo { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
