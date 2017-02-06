using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Location
    {
        [Key]
        public Guid LocationId { get; set; }
        [Required]
        public int GalaxyNumber { get; set; }
        [Required]
        public int LocalCluster { get; set; }
        public int? SystemNumber { get; set; }
    }
}