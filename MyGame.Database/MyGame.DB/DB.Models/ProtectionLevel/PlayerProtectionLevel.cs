using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.ProtectionLevel
{
   public class PlayerProtectionLevel
    {
        [Key, Column(Order = 0)]
        public Guid PlayerId { get; set; }


        [Key, Column(Order = 1)]
        public int ProtectionLevelID { get; set; }


        public virtual Players Player { get; set; }
        public virtual ProtectionLevel ProtectionLevel { get; set; }
        [Required]
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public string Source { get; set; }
    }
}
