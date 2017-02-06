using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.ProtectionLevel
{
    public class ProtectionLevel
    {
        [Key]
        public int ProtectionLevelID { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProtectionLevelName { get; set; }
    }
}
