using MyGame.DB.DB.Models.Market;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Troops
    {
        [Key]
        public Guid TroopID { get; set; }
        [Required]
        [MaxLength(50)]
        public string TroopName { get; set; }
        [Required]
        public int AttackValue { get; set; }
        [Required]
        public int DefenseValue { get; set; }
        public int Armor { get; set; }
        [Required]
        public int Health { get; set; }
        [Required]
        public int AbleToCarry { get; set; }
        [Required]
        public int PeopleToOperate { get; set; }
        public Guid MarketContentRefId { get; set; }
        [ForeignKey("MarketContentRefId")]
        public MarketContent Price { get; set; }
    }
}