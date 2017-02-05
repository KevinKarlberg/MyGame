using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Terrain
    {
        [Key]
        public Guid TerrainId { get; set; }
        [Required]
        [MaxLength(50)]
        public string TerrainType { get; set; }
        [Required]
        [MaxLength(500)]
        public string TerrainDescription { get; set; }
    }
}