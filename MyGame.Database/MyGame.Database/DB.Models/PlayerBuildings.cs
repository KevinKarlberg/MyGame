using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class PlayerBuildings
    {
        [Key, Column(Order = 0)]
        public Guid PlayerId { get; set; }
        [ForeignKey("Building")]
        [Key, Column(Order = 1)]
        public Guid BuildingId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Buildings Building { get; set; }
        public virtual Players Player { get; set; }
    }
}