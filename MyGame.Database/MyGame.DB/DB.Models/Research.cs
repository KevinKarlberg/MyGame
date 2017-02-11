using MyGame.DB.DB.Models.Market;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Research
    {
        [Key]
        public Guid ResearchId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ResearchName { get; set; }
        public Guid MarketContentRefId { get; set; }
        [ForeignKey("MarketContentRefId")]
        public virtual MarketContent Price { get; set; }
    }
}