using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class Planets
    {
        [Key]
        public Guid PlanetId { get; set; }
        [Required]
        [MaxLength(50)]
        public string PlanetName { get; set; }
        [ForeignKey("Terrain")]
        public Guid TerrainId { get; set; }

        public virtual Terrain Terrain { get; set; }
    }
}