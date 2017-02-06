using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class PlayerShips
    {
        [Key, Column(Order = 0)]
        public Guid ShipId { get; set; }
        [Key, Column(Order = 1)]
        public Guid PlayerId { get; set; }
        [Key, Column(Order = 2)]
        public Location LocationId { get; set; }
        public DateTime? Arriving { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Ships Ships { get; set; }
        public virtual Players Players { get; set; }
        public virtual Location Location { get; set; }
    }
}