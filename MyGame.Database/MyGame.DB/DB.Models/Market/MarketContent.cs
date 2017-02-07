using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.Market
{
    public class MarketContent
    {
        [Key, Column(Order = 0)]
        public Guid MarketContentID { get; set; }
        public int? Oil { get; set; }
        public int? Minerals { get; set; }
        public int? Credits { get; set; }
        public int? SpecialCredits { get; set; }
        public int? SpecialResource { get; set; }
    }
}
