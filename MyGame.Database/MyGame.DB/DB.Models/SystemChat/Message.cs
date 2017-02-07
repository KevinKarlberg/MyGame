using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.SystemChat
{
   public class Message
    {
        [Key]
        public Guid MessageId { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }
        // F-Key
        public int PlayerRefID { get; set; }
        [ForeignKey(name: "PlayerRefID")]
        public virtual Players Player { get; set; }
    }
}
