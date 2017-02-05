using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Buildings
    {
        [Key]
        public Guid BuildingId { get; set; }
        [Required]
        [MaxLength(50)]
        public string BuildingName { get; set; }
    }
}