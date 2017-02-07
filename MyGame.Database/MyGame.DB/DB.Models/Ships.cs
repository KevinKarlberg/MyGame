using MyGame.DB.DB.Models.Market;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Ships
    {
        [Key]
        public Guid ShipId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ShipName { get; set; }
        [Required]
        public Guid MarketContentRefId { get; set; }
        [ForeignKey("MarketContentRefId")]
        public virtual MarketContent Cost { get; set; }
    }
}