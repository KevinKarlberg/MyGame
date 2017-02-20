using MyGame.DB.DB.Models.Market;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models
{
   public class Missions
    {
        [Key]
        public Guid MissionId { get; set; }
        [Required]
        public Guid MissionTypeRefId { get; set; }
        [Required]
        public DateTime Started { get; set; }
        [Required]
        public DateTime Landing { get; set; }
        public DateTime? Returning { get; set; }
        public DateTime? Returned { get; set; }
        [Required]
        public Guid PlayerRefId { get; set; }
        public Guid LocationRefId { get; set; }
        public Guid? MarketContentRefId { get; set; }
        public virtual ICollection<PlayerShips> Ships { get; set; }
        public virtual ICollection<PlayerTroops> Troops { get; set; }
        [ForeignKey("PlayerRefId")]
        public virtual Players Player { get; set; }
        [ForeignKey("MissionTypeRefId")]
        public virtual MissionTypes MissionType { get; set; }
        [ForeignKey("LocationRefId")]
        public virtual Location Target { get; set; }
        [ForeignKey("MarketContentRefId")]
        public virtual MarketContent CarryingResources { get; set; }
    }
}
