using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.Mailfunction
{
    public class Mail
    {
        [Key]
        public int MailID { get; set; }
        [Required]
        [MaxLength(500)]
        public string MailContentMessage { get; set; }

        [Required]
        public DateTime Sent { get; set; }
    }
}
