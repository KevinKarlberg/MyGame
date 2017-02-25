using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYGame.DBv2.Models
{
   public class Buying
    {
        [Key, Column(Order = 0)]
        public Guid BuyingID { get; set; }

        public Guid BuyingMarketContentRefId { get; set; }
        [ForeignKey("BuyingMarketContentRefId")]
        public virtual MarketContent MarketContent { get; set; }
    }
}
