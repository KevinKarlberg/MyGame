using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyGame.Database.DB.Models
{
    public class PlayerPlanets
    {
        [Key, Column(Order = 0)]
        public Guid PlayerId { get; set; }
        [Key, Column(Order = 1)]
        public Guid PlanetId { get; set; }
        [Required]
        public int CurrentSize { get; set; }
        [Required]
        public int MaxSize { get; set; }

        public virtual Players Player { get; set; }
        public virtual Planets Planet { get; set; }
    }
}