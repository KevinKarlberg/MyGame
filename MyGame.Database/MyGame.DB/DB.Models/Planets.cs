using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Planets
    {
        [Key]
        public Guid PlanetId { get; set; }
        [Required]
        [MaxLength(50)]
        public string PlanetName { get; set; }
        [Required]
        public int Size { get; set; }
        public Guid TerrainRefId { get; set; }
        [ForeignKey("TerrainRefId")]
        public virtual Terrain Terrain { get; set; }
    }
}