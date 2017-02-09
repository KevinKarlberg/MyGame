using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models
{
    class BuildingStructureEffects
    {
        [Key, Column(Order = 0)]
        public Guid BuildingId { get; set; }
        [Key, Column(Order = 1)]
        public Guid StructureEffectId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
