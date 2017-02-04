using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class PlayerShips
    {
        [Key, Column(Order = 0)]
        public Guid ShipId { get; set; }
        [Key, Column(Order = 1)]
        public Guid PlayerId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Ships Ships { get; set; }
        public virtual Players Players { get; set; }
    }
}