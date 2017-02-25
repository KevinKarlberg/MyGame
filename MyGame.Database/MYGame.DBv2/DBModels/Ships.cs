
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MYGame.DBv2.Models
{
    public class Ships
    {
        [Key]
        public Guid ShipId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ShipName { get; set; }
        [Required]
        public int AttackValue { get; set; }
        [Required]
        public int DefenseValue { get; set; }
        public int Armor { get; set; }
        [Required]
        public int Health { get; set; }
        [Required]
        public int GroundTroopCapacity { get; set; }
        [Required]
        public int PeopleToOperate { get; set; }
        [Required]
        public int Speed { get; set; }
        [Required]
        public Guid MarketContentRefId { get; set; }
        [ForeignKey("MarketContentRefId")]
        public virtual MarketContent Cost { get; set; }
    }
}