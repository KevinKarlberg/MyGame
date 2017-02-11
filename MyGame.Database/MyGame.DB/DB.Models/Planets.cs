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
        public int? PlanetNumber { get; set; }
        [Required]
        public int CurrentSize { get; set; }
        [Required]
        public int MaxSize { get; set; }
        [Required]
        public int? CurrentPopulation { get; set; }
        [Required]
        public int? MaxPopulation { get; set; }
        [Required]
        public int Free { get; set; }
        [Required]
        public int Occupied { get; set; }
        public Guid TerrainRefId { get; set; }
        [ForeignKey("TerrainRefId")]
        public virtual Terrain Terrain { get; set; }
    }
}