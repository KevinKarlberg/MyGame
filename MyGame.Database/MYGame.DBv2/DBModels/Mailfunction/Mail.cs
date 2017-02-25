using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYGame.DBv2.Models
{
    public class Mail
    {
        [Key]
        public Guid MailID { get; set; }
        [Required]
        [MaxLength(50)]
        public string From { get; set; }
        [Required]
        [MaxLength(500)]
        public string MailContentMessage { get; set; }

        [Required]
        public DateTime Sent { get; set; }
    }
}
