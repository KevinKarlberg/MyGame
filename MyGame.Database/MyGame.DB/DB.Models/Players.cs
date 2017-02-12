using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
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
        public int ElectricityAvailable { get; set; }
        public Guid RaceRefId { get; set; }
        public Guid? LocationRefId { get; set; }
        [ForeignKey("RaceRefId")]
        public virtual Races Race { get; set; }
        [ForeignKey("LocationRefId")]
        public virtual Location Location { get; set; }
        public int ElectricityLevel { get; internal set; }
    }
}