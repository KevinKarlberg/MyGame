using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.SystemChat
{
  public  class Chat
    {
        [Key]
        public int ChatID { get; set; }
        [MaxLength(500)]
        public DateTime TimeStamp { get; set; }

        // F-Key
        public int PlayerRefId { get; set; }
        [ForeignKey(name: "PlayerRefID")]
        public virtual Players Player { get; set; }
         // F-Key
        public int LocationRefId { get; set; }
        [ForeignKey(name: "LocationRefId")]
        public virtual Location Location { get; set; }
        public int MessageRefId { get; set; }
        [ForeignKey(name: "MessageRefId")]
        public virtual Message Message { get; set; }
    }
}
