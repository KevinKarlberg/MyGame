using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class Players
    {
        [Key]
        public Guid PlayerId { get; set; }
        [Required]
        public Guid PlayerAccountId { get; set; }
        [Required]
        [MaxLength(50)]
        public string EmpireName { get; set; }
        [Required]
        public int TradeBalance { get; set; }
        [Required]
        public int EmpireStrength { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int ElectricityLevel { get; set; }
        [ForeignKey("Race")]
        public Guid RaceId { get; set; }
        [ForeignKey("Location")]
        public Guid LocationId { get; set; }
        public virtual Races Race { get; set; }
        public virtual Location Location { get; set; }
    }
}