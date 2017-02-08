using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models
{
    class RacesRacialTraits
    {
        [Key,Column(Order  = 0)]
        public Guid RaceId { get; set; }
        [Key, Column(Order = 1)]
        public Guid RacialTraitsId { get; set; }
        [Required]
        public int? ModifierAmount { get; set; }
    }
}
