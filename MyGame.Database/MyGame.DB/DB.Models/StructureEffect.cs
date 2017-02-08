using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models
{
    class StructureEffect
    {
        [Key]
        public Guid StructureEffectId { get; set; }
        [Required]
        public string StructureEffectName { get; set; }
    }
}
