using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.Mailfunction
{
   public class PlayerMail
    {
        [Key, Column(Order = 0)]
        public Guid SendingPlayerId { get; set; }

        [Key, Column(Order = 1)]
        public Guid RecievingPlayerId { get; set; }

        [Key, Column(Order = 2)]
        public Guid MailID { get; set; }


        public virtual Players Sender { get; set; }
        public virtual Players Reciever { get; set; }
        public virtual Mail Mail { get; set; }
        [Required]
        public bool IsRead { get; set; }
    }
}
