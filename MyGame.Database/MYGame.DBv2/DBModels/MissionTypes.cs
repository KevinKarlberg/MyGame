using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYGame.DBv2.Models
{
    public class MissionTypes
    {
        [Key]
        public Guid MissionTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string MissionTypeName { get; set; }
        [Required]
        [MaxLength(50)]
        public string MissionTypeCategory { get; set; }


    }
}
