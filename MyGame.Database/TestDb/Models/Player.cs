using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDb.Models
{
    public class Player
    {
        [Key]
        public Guid PlayerId { get; set; }
        [Required]
        public Guid PlayerAccountId { get; set; }
        [Required]
        [MaxLength(50)]
        public string EmpireName { get; set; }
        [Required]
        public int TradeBalance { get; set; }
        [Required]
        public int EmpireStrength { get; set; }
        [Required]
        public int Level { get; set; }
        public virtual ICollection<Ship> Ships { get; set; }

    }
}
