using MyGame.DB.DB.Models.Market;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Buildings
    {
        [Key]
        public Guid BuildingId { get; set; }
        [Required]
        public int SpaceRequired { get; set; }
        [Required]
        [MaxLength(50)]
        public string BuildingName { get; set; }
        public Guid? TerrainRefId { get; set; }
        [Required]
        public Guid MarketContentRefId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [ForeignKey("MarketContentRefId")]
        public MarketContent Price { get; set; }
        [ForeignKey("TerrainRefId")]
        public Terrain TerrainRequired { get; set; }
    }
}