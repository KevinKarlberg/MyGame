using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MYGame.DBv2.Models
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