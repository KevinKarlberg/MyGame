using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDb.Models
{
    public class Ship
    {
        [Key]
        public Guid ShipId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ShipName { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int AttackValue { get; set; }
        [Required]
        public int DefenseValue { get; set; }
        public int Armor { get; set; }
        [Required]
        public int Health { get; set; }
        [Required]
        public int GroundTroopCapacity { get; set; }
        [Required]
        public int PeopleToOperate { get; set; }
        [Required]
        public int Speed { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
