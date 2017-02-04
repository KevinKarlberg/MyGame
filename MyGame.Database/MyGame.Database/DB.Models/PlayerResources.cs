using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class PlayerResources
    {
        [Key, Column(Order = 0)]
        public Guid PlayerId { get; set; }
        [Key, Column(Order = 1)]
        public Guid ResourcesId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Players Player { get; set; }
        public virtual Resources Resource { get; set; }
    }
}