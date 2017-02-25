using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MYGame.DBv2.Models
{
    public class PlayerTroops
    {
        [Key, Column(Order = 0)]
        public Guid TroopId { get; set; }
        [Key, Column(Order = 1)]
        public Guid PlayerId { get; set; }
        [Key, Column(Order = 2)]
        public DateTime? Arriving { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual Troops Troops { get; set; }
        public virtual Players Players { get; set; }
    }
}