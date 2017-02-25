﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MYGame.DBv2.Models
{
    public class Races
    {
        [Key]
        public Guid RaceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string RaceName { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public string RacialTraits { get; set; }
    }
}