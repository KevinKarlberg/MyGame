using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class Resources
    {
        [Key]
        public Guid ResourceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ResourceName { get; set; }
    }
}