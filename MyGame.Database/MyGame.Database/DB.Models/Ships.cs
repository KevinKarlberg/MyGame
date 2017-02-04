using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class Ships
    {
        [Key]
        public Guid ShipId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ShipName { get; set; }
    }
}