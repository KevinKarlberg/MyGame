using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class Troops
    {
        [Key]
        public Guid TroopID { get; set; }
        [Required]
        [MaxLength(50)]
        public string TroopName { get; set; }
    }
}