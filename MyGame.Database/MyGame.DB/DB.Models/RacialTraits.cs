﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models
{
    class RacialTraits
    {
        [Key]
        public Guid RacialTraitsId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ModifierName { get; set; }


    }
}
