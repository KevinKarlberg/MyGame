using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGame.DB.DB.Models
{
    public class Research
    {
        [Key]
        public Guid ResearchId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ResearchName { get; set; }
    }
}