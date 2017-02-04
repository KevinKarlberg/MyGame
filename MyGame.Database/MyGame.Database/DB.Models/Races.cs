using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class Races
    {
        [Key]
        public Guid RaceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string RaceName { get; set; }
    }
}